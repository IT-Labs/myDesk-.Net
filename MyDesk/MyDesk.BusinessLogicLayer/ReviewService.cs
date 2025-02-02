﻿using AutoMapper;
using MyDesk.Data.Interfaces.BusinessLogic;
using MyDesk.Data.Interfaces.Repository;
using MyDesk.Data.DTO;
using MyDesk.Data.Entities;
using MyDesk.Data.Exceptions;
using Newtonsoft.Json;
using System.Text;
using MyDesk.Data.Responses;
using Microsoft.Extensions.Configuration;

namespace MyDesk.BusinessLogicLayer
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public ReviewService(IReviewRepository reviewRepository,
            IReservationRepository reservationRepository,
            IConfiguration config,
            IMapper mapper,
            IHttpClientFactory clientFactory)
        {
            _reviewRepository = reviewRepository;
            _reservationRepository = reservationRepository;
            _config = config;
            _mapper = mapper;
            _httpClient = clientFactory.CreateClient();
        }

        public List<ReviewDto> GetReviewsForDesk(int id)
        {
            List<Reservation> deskReservations = _reservationRepository.GetPastDeskReservations(id, includeReview: true);
            List<Review> reviews = deskReservations.SelectMany(x => x.Reviews).ToList();

            return _mapper.Map<List<ReviewDto>>(reviews);
        }

        public ReviewDto ShowReview(int id)
        {
            Review review = _reviewRepository.Get(id);
            if (review == null)
            {
                throw new NotFoundException($"Review with ID: {id} not found.");
            }

            return _mapper.Map<ReviewDto>(review);
        }

        public List<ReviewDto> AllReviews(int? take = null, int? skip = null)
        {
            List<Review> reviews = _reviewRepository.GetAll(take: take, skip: skip);

            foreach (Review review in reviews)
            {
                // EF will handle references
                // We used foreach instead of SQL join in order to prevent inefficient queries
                review.Reservation = _reservationRepository.Get(review.ReservationId, includeDesk: true, includeonferenceRoom: true, includeOffice: true);
            }

            return _mapper.Map<List<ReviewDto>>(reviews);
        }

        public void CreateReview(ReviewDto reviewDto)
        {
            Reservation reservation = _reservationRepository.Get(reviewDto?.Reservation?.Id??0);
            if (reservation == null)
            {
                throw new NotFoundException($"Reservation with ID: {reviewDto?.Reservation?.Id ?? 0} not found");
            }

            Review review = new Review()
            {
                Reviews = reviewDto?.Reviews,
                ReservationId = reviewDto?.Reservation?.Id??0,
                ReviewOutput = GetAnalysedReview(reviewDto?.Reviews??string.Empty)
            };

            _reviewRepository.Insert(review);
        }

        private string GetAnalysedReview(string textReview)
        {
            string review = string.Empty;
            string sentimentEndpoint = _config["SentimentEndpoint"];

            if (!string.IsNullOrEmpty(sentimentEndpoint))
            {
                Dictionary<string, string> dictionaryData = new () { { "text", textReview } };
                string data = JsonConvert.SerializeObject(dictionaryData);

                HttpRequestMessage request = new ()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(sentimentEndpoint),
                    Content = new StringContent(data, Encoding.UTF8, "application/json")
                };
                HttpResponseMessage response = _httpClient.Send(request, CancellationToken.None);

                if (response.IsSuccessStatusCode)
                {
                    string stringResponse = response.Content.ReadAsStringAsync().Result;
                    ReviewAzureFunction? result = JsonConvert.DeserializeObject<ReviewAzureFunction>(stringResponse);

                    review = result?.Sentiment??string.Empty;
                }
            }

            return review;
        }
    }
}

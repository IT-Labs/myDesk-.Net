﻿using MyDesk.Repository;
using MyDesk.Data.Interfaces.Repository;
using MyDesk.Data.Entities;
using NUnit.Framework;

namespace MyDesk.UnitTests.Repository
{
    public class ReservationRepositoryTests : RepositoryBaseTest
    {
        private IReservationRepository _reservationRepository;

        [SetUp]
        public void Setup()
        {
            _reservationRepository = new ReservationRepository(base._dbContext);
        }

        [TearDown]
        public void CleanUp()
        {
            base.CleanDbContext();
        }

        [TestCase(1, null, null, null, null)]
        [TestCase(1, true, null, null, null)]
        [TestCase(1, true, true, null, null)]
        [TestCase(1, true, true, null, true)]
        [TestCase(4, null, null, null, null)]
        [TestCase(4, null, null, true, null)]
        [TestCase(4, null, true, true, null)]
        [TestCase(4, null, true, true, true)]
        [Order(1)]
        public void Get_Success(int id, bool? includeDesk, bool? includeOffice, bool? includeonferenceRoom, bool? includeReviews)
        {
            // Arrange + Act
            Reservation reservation = _reservationRepository.Get(id, includeDesk: includeDesk, includeOffice: includeOffice, 
                includeonferenceRoom: includeonferenceRoom, includeReviews: includeReviews);

            // Assert
            Assert.NotNull(reservation, "Reservation should exist.");

            if (includeReviews == true)
            {
                Assert.IsTrue(reservation.Reviews.Count == 1);
            }
            else
            {
                Assert.IsTrue(reservation.Reviews.Count == 0);
            }

            if (includeDesk == true)
            {
                Assert.NotNull(reservation.Desk);
                if (includeOffice == true)
                {
                    Assert.NotNull(reservation.Desk.Office);
                }
                else
                {
                    Assert.IsNull(reservation.Desk.Office);
                }
            }
            else
            {
                Assert.IsNull(reservation.Desk);
            }

            if (includeonferenceRoom == true)
            {
                Assert.NotNull(reservation.ConferenceRoom);
                if (includeOffice == true)
                {
                    Assert.NotNull(reservation.ConferenceRoom.Office);
                }
                else
                {
                    Assert.IsNull(reservation.ConferenceRoom.Office);
                }
            }
            else
            {
                Assert.IsNull(reservation.ConferenceRoom);
            }
        }

        [Test]
        [Order(2)]
        public void Get_Failure()
        {
            // Arrange + Act
            Reservation reservation = _reservationRepository.Get(2);

            // Assert
            Assert.IsNull(reservation, "Reservation shouldn't exist.");
        }

        [TestCase(null, null)]
        [TestCase(true, null)]
        [TestCase(null, true)]
        [TestCase(true, true)]
        [Order(3)]
        public void GetEmployeeReservations_Success(bool? includeDesk, bool? includeConferenceRoom)
        {
            // Arrange + Act
            List<Reservation> reservations = _reservationRepository.GetEmployeeReservations(2, includeDesk: includeDesk,
                includeConferenceRoom: includeConferenceRoom);

            // Assert
            Assert.IsTrue(reservations.Count == 4);
            Assert.IsTrue(reservations.All(x => x.StartDate >= DateTime.Now.Date || x.EndDate >= DateTime.Now.Date));

            foreach (Reservation reservation in reservations)
            {
                if (includeDesk == true && reservation.DeskId.HasValue)
                {
                    Assert.NotNull(reservation.Desk);
                }
                else
                {
                    Assert.IsNull(reservation.Desk);
                }

                if (includeConferenceRoom == true && reservation.ConferenceRoomId.HasValue)
                {
                    Assert.NotNull(reservation.ConferenceRoom);
                }
                else
                {
                    Assert.IsNull(reservation.ConferenceRoom);
                }
            }
        }

        [TestCase(null, null, null, null, null, null)]
        [TestCase(null, true, null, null, null, null)]
        [TestCase(true, true, null, true, null, null)]
        [TestCase(true, null, true, null, null, null)]
        [TestCase(true, null, true, true, 1, 0)]
        [TestCase(false, null, true, true, 2, 0)]
        [Order(4)]
        public void GetEmployeeFutureReservations_Success(bool? includeEmployee, bool? includeDesk, bool? includeConferenceRoom, bool? includeOffice, int? take, int? skip)
        {
            // Arrange + Act
            Tuple<int?, List<Reservation>> result = _reservationRepository.GetFutureReservations(2, includeEmployee: includeEmployee, includeDesk: includeDesk, 
                includeConferenceRoom: includeConferenceRoom, includeOffice: includeOffice, take: take, skip: skip);

            // Assert
            if (take.HasValue && skip.HasValue)
            {
                Assert.IsTrue(result.Item1 == 3);
                Assert.IsTrue(result.Item2.Count == take.Value);
            }
            else
            {
                Assert.IsNull(result.Item1);
                Assert.IsTrue(result.Item2.Count == 3);
            }
            
            Assert.IsTrue(result.Item2.All(x => x.StartDate >= DateTime.Now.Date));

            foreach (Reservation reservation in result.Item2)
            {
                if (includeEmployee == true)
                {
                    Assert.NotNull(reservation.Employee);
                }

                if (includeDesk == true && reservation.DeskId.HasValue)
                {
                    Assert.NotNull(reservation.Desk);
                    if (includeOffice == true)
                    {
                        Assert.NotNull(reservation.Desk.Office);
                    }
                    else
                    {
                        Assert.IsNull(reservation.Desk.Office);
                    }
                }
                else
                {
                    Assert.IsNull(reservation.Desk);
                }

                if (includeConferenceRoom == true && reservation.ConferenceRoomId.HasValue)
                {
                    Assert.NotNull(reservation.ConferenceRoom);
                    if (includeOffice == true)
                    {
                        Assert.NotNull(reservation.ConferenceRoom.Office);
                    }
                    else
                    {
                        Assert.IsNull(reservation.ConferenceRoom.Office);
                    }
                }
                else
                {
                    Assert.IsNull(reservation.ConferenceRoom);
                }
            }
        }

        [TestCase(null, null, null, null, null, null, null)]
        [TestCase(null, true, null, null, null, null, null)]
        [TestCase(true, true, null, true, null, null, null)]
        [TestCase(true, null, true, null, null, null, null)]
        [TestCase(true, null, true, true, null, null, null)]
        [TestCase(false, null, true, true, true, 1, 0)]
        [Order(5)]
        public void GetEmployeePastReservations_Success(bool? includeEmployee, bool? includeDesk, bool? includeConferenceRoom, bool? includeOffice, bool? includeReviews, int? take, int? skip)
        {
            // Arrange + Act
            Tuple<int?, List<Reservation>> result = _reservationRepository.GetPastReservations(2, includeEmployee: includeEmployee, includeDesk: includeDesk,
                includeConferenceRoom: includeConferenceRoom, includeOffice: includeOffice, includeReviews: includeReviews, take: take, skip: skip);

            // Assert
            if (take.HasValue && skip.HasValue)
            {
                Assert.IsTrue(result.Item1 == 2);
                Assert.IsTrue(result.Item2.Count == take.Value);
            }
            else
            {
                Assert.IsNull(result.Item1);
                Assert.IsTrue(result.Item2.Count == 2);
            }
            
            Assert.IsTrue(result.Item2.All(x => x.StartDate < DateTime.Now.Date && x.EndDate < DateTime.Now.Date));

            foreach (Reservation reservation in result.Item2)
            {
                if (includeEmployee == true)
                {
                    Assert.NotNull(reservation.Employee);
                }

                if (includeReviews == true)
                {
                    Assert.IsTrue(reservation.Reviews.Count == 1);
                }
                else
                {
                    Assert.IsTrue(reservation.Reviews.Count == 0);
                }

                if (includeDesk == true && reservation.DeskId.HasValue)
                {
                    Assert.NotNull(reservation.Desk);
                    if (includeOffice == true)
                    {
                        Assert.NotNull(reservation.Desk.Office);
                    }
                    else
                    {
                        Assert.IsNull(reservation.Desk.Office);
                    }
                }
                else
                {
                    Assert.IsNull(reservation.Desk);
                }

                if (includeConferenceRoom == true && reservation.ConferenceRoomId.HasValue)
                {
                    Assert.NotNull(reservation.ConferenceRoom);
                    if (includeOffice == true)
                    {
                        Assert.NotNull(reservation.ConferenceRoom.Office);
                    }
                    else
                    {
                        Assert.IsNull(reservation.ConferenceRoom.Office);
                    }
                }
                else
                {
                    Assert.IsNull(reservation.ConferenceRoom);
                }
            }
        }

        [TestCase(null)]
        [TestCase(true)]
        [Order(6)]
        public void GetDeskReservations_Success(bool? includeEmployee)
        {
            // Arrange + Act
            List<Reservation> result = _reservationRepository.GetDeskReservations(1, includeEmployee: includeEmployee);

            // Assert
            Assert.IsTrue(result.Count == 1);

            Reservation reservation = result.Single();
            if (includeEmployee == true)
            {
                Assert.NotNull(reservation.Employee);
            }
            else
            {
                Assert.IsNull(reservation.Employee);
            }
        }

        [TestCase(null)]
        [TestCase(true)]
        [Order(7)]
        public void GetPastDeskReservations_Success(bool? includeReview)
        {
            // Arrange + Act
            List<Reservation> result = _reservationRepository.GetPastDeskReservations(1, includeReview: includeReview);

            // Assert
            Assert.IsTrue(result.Count == 1);

            Reservation reservation = result.Single();
            if (includeReview == true)
            {
                Assert.NotNull(reservation.Reviews.Count == 1);
            }
            else
            {
                Assert.IsNull(reservation.Employee);
            }
        }

        [Test]
        [Order(8)]
        public void Insert_Success()
        {
            // Arrange
            Reservation reservation = new Reservation()
            {
                DeskId = 3,
                EmployeeId = 2,
                StartDate = DateTime.Now.AddDays(5),
                EndDate = DateTime.Now.AddDays(10),
                IsDeleted = false
            };

            // Act
            _reservationRepository.Insert(reservation);

            // Assert
            Reservation createdReservation = _reservationRepository.Get(reservation.Id);

            Assert.NotNull(createdReservation, "Reservation should not be null.");
            Assert.IsTrue(createdReservation.Id == reservation.Id);
        }

        [Test]
        [Order(9)]
        public void SoftDelete_Success()
        {
            // Arrange
            int id = 4;
            Reservation reservation = _reservationRepository.Get(id);

            // Act
            _reservationRepository.SoftDelete(reservation);

            // Assert
            Reservation deletedReservation = _reservationRepository.Get(id);
            Assert.IsNull(deletedReservation, "Reservation should be null.");
        }
    }
}
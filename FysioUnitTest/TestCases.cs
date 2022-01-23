using System;
using System.Collections.Generic;
using System.Linq;
using AvansFysioAppDomain.Domain;
using AvansFysioAppDomain.Domain.DataAnnotations;
using AvansFysioAppDomainServices.DomainServices;
using AvansFysioAppDomainServices.Validation;
using AvansFysioAppInfrastructure.Repos;
using Moq;
using Xunit;
using Physiotherapist = AvansFysioAppDomain.Domain.Physiotherapist;

namespace FysioUnitTest
{
    public class TestCases
    {
        private readonly Mock<SessionIRepo> sessionService;
        private readonly Mock<TreatmentPlanIRepo> treatmentPlanService;
        private readonly AgeAnnotions ageAnnotions;
        private readonly AppointmentValidation appointmentValidation = new AppointmentValidation();
        private readonly PatientValidation patientValidation = new PatientValidation();

        public TestCases()
        {
            ageAnnotions = new AgeAnnotions();
            sessionService = new Mock<SessionIRepo>();
            treatmentPlanService = new Mock<TreatmentPlanIRepo>();
        }

        [Fact]
        public void BR_1()
        {
            //Arrange
            Physiotherapist phy = new Physiotherapist()
            {
                AvailabilityEnd = new DateTime(1, 1, 1, 8, 0, 0),
                AvailabilityStart = new DateTime(1, 1, 1, 20, 0, 0),
                BigId = 1234,
                Email = "abc@abcd.nl",
                EmployeeId = 5,
                Id = 1,
                IsIntern = false,
                Name = "Diren",
                Phone = "0612345678"
            };

            Patient patient = new Patient()
            {
                Birthday = new DateTime(2001, 4, 4),
                Email = "diren@hotmail.com",
                Gender = "Male",
                Name = "Diren",
                Occupation = "Student",
                OccupationId = 2158837,
                PatientId = 1,
                Phone = "0612345678",
                Picture = null,
                PictureFormat = null
            };

            PatientFile file = new PatientFile()
            {
                Age = 20,
                DateOfEnd = new DateTime(2023, 1, 1),
                DateOfRegister = DateTime.Now,
                DescriptionComplaintsGlobal = "This is a test",
                DiagnosisDescription = "This is a string",
                DiagnosisNumber = "1000",
                HeadPractitioner = phy,
                HeadPractitionerId = 1,
                Id = 1,
                IntakeBy = phy,
                IntakeById = 1,
                SupervisionBy = null,
                SupervisionById = null,
                Patient = patient,
                PatientId = 1,
                Remarks = new List<Remark>(),
                Treatments = new List<Treatment>(),
                Title = "Diren's file"
            };

            TreatmentPlan treatmentPlan = new TreatmentPlan()
            {
                Id = 1,
                Duration = 30,
                File = file,
                FileId = 1,
                MaxSessions = 1,
                Physiotherapist = phy,
                PhysiotherapistId = 1,
                Title = "A Plan With Diren"
            };

            Session session1 = new Session()
            {
                AppointmentBegin = new DateTime(2022, 5, 5, 15, 30, 0),
                AppointmentEnd = new DateTime(2022, 5, 5, 16, 0, 0),
                AppointmentMade = DateTime.Now,
                HeadPhysiotherapist = phy,
                HeadPhysiotherapistId = 1,
                Id = 1,
                Patient = patient,
                PatientId = 1,
                TreatmentPlan = treatmentPlan,
                TreatmentPlanId = 1,
            };

            Session session2 = new Session()
            {
                AppointmentBegin = new DateTime(2022, 5, 5, 15, 30, 0),
                AppointmentEnd = new DateTime(2022, 5, 5, 16, 0, 0),
                AppointmentMade = DateTime.Now,
                HeadPhysiotherapist = phy,
                HeadPhysiotherapistId = 1,
                Id = 1,
                Patient = patient,
                PatientId = 1,
                TreatmentPlan = treatmentPlan,
                TreatmentPlanId = 1,
            };

            Session session3 = new Session()
            {
                AppointmentBegin = new DateTime(2022, 6, 5, 15, 30, 0),
                AppointmentEnd = new DateTime(2022, 6, 5, 16, 0, 0),
                AppointmentMade = DateTime.Now,
                HeadPhysiotherapist = phy,
                HeadPhysiotherapistId = 1,
                Id = 1,
                Patient = patient,
                PatientId = 1,
                TreatmentPlan = treatmentPlan,
                TreatmentPlanId = 1,
            };

            treatmentPlanService.Setup(treatmentPlanRepository => treatmentPlanRepository.TreatmentPlans()).Returns(new List<TreatmentPlan>(){treatmentPlan});
            sessionService.Setup(sessionRepo => sessionRepo.GetSessionsWithTreatmentPlanId(treatmentPlan.Id)).Returns(new List<Session>(){session1});

            //Act
            List<Session> sessions = sessionService.Object.GetSessionsWithTreatmentPlanId(treatmentPlan.Id).ToList();
            bool invalid  = appointmentValidation.CheckIfSessionsPerWeekWillBeExceeded(session2, treatmentPlan.MaxSessions, sessions);
            bool invalid2 = appointmentValidation.CheckIfSessionsPerWeekWillBeExceeded(session3, treatmentPlan.MaxSessions, sessions);



            //Assert
            Assert.True(invalid);
        }

        [Fact]
        public void BR_2()
        {
            //Arrange
            Physiotherapist phy = new Physiotherapist()
            {
                AvailabilityEnd = new DateTime(1, 1, 1, 8, 0, 0),
                AvailabilityStart = new DateTime(1, 1, 1, 20, 0, 0),
                BigId = 1234,
                Email = "abc@abcd.nl",
                EmployeeId = 5,
                Id = 1,
                IsIntern = false,
                Name = "Diren",
                Phone = "0612345678"
            };

            Patient patient = new Patient()
            {
                Birthday = new DateTime(2001, 4, 4),
                Email = "diren@hotmail.com",
                Gender = "Male",
                Name = "Diren",
                Occupation = "Student",
                OccupationId = 2158837,
                PatientId = 1,
                Phone = "0612345678",
                Picture = null,
                PictureFormat = null
            };

            PatientFile file = new PatientFile()
            {
                Age = 20,
                DateOfEnd = new DateTime(2023, 1, 1),
                DateOfRegister = DateTime.Now,
                DescriptionComplaintsGlobal = "This is a test",
                DiagnosisDescription = "This is a string",
                DiagnosisNumber = "1000",
                HeadPractitioner = phy,
                HeadPractitionerId = 1,
                Id = 1,
                IntakeBy = phy,
                IntakeById = 1,
                SupervisionBy = null,
                SupervisionById = null,
                Patient = patient,
                PatientId = 1,
                Remarks = new List<Remark>(),
                Treatments = new List<Treatment>(),
                Title = "Diren's file"
            };

            TreatmentPlan treatmentPlan = new TreatmentPlan()
            {
                Id = 1,
                Duration = 30,
                File = file,
                FileId = 1,
                MaxSessions = 1,
                Physiotherapist = phy,
                PhysiotherapistId = 1,
                Title = "A Plan With Diren"
            };

            Session session1 = new Session()
            {
                AppointmentBegin = new DateTime(2022, 5, 5, 15, 30, 0),
                AppointmentEnd = new DateTime(2022, 5, 5, 16, 0, 0),
                AppointmentMade = DateTime.Now,
                HeadPhysiotherapist = phy,
                HeadPhysiotherapistId = 1,
                Id = 1,
                Patient = patient,
                PatientId = 1,
                TreatmentPlan = treatmentPlan,
                TreatmentPlanId = 1,
            };

            Session session2 = new Session()
            {
                AppointmentBegin = new DateTime(2022, 5, 5, 15, 30, 0),
                AppointmentEnd = new DateTime(2022, 5, 5, 16, 0, 0),
                AppointmentMade = DateTime.Now,
                HeadPhysiotherapist = phy,
                HeadPhysiotherapistId = 1,
                Id = 1,
                Patient = patient,
                PatientId = 1,
                TreatmentPlan = treatmentPlan,
                TreatmentPlanId = 1,
            };

            Session session3 = new Session()
            {
                AppointmentBegin = new DateTime(2022, 6, 5, 15, 30, 0),
                AppointmentEnd = new DateTime(2022, 6, 5, 16, 0, 0),
                AppointmentMade = DateTime.Now,
                HeadPhysiotherapist = phy,
                HeadPhysiotherapistId = 1,
                Id = 1,
                Patient = patient,
                PatientId = 1,
                TreatmentPlan = treatmentPlan,
                TreatmentPlanId = 1,
            };

            sessionService.Setup(sessionRepo => sessionRepo.GetSessionsWithPhysiotherapistId(phy.Id)).Returns(new List<Session>() { session1 });

            //Act
            List<Session> sessions = sessionService.Object.GetSessionsWithPhysiotherapistId(phy.Id).ToList();

            //Physiotherapist heeft niet de tijd
            bool unavailable = appointmentValidation.PhysioIsAvailable(sessions, session2, phy);
            //Physiotherapist heeft de tijd
            bool unavailable2 = appointmentValidation.PhysioIsAvailable(sessions, session3, phy);


            //Assert
            Assert.False(unavailable);
            Assert.False(unavailable2);
        }


        [Fact]
        public void BR_3()
        {
            Physiotherapist phy = new Physiotherapist()
            {
                AvailabilityEnd = new DateTime(1, 1, 1, 8, 0, 0),
                AvailabilityStart = new DateTime(1, 1, 1, 20, 0, 0),
                BigId = 1234,
                Email = "abc@abcd.nl",
                EmployeeId = 5,
                Id = 1,
                IsIntern = false,
                Name = "Diren",
                Phone = "0612345678"
            };

            Patient patient = new Patient()
            {
                Birthday = new DateTime(2001, 4, 4),
                Email = "diren@hotmail.com",
                Gender = "Male",
                Name = "Diren",
                Occupation = "Student",
                OccupationId = 2158837,
                PatientId = 1,
                Phone = "0612345678",
                Picture = null,
                PictureFormat = null
            };

            PatientFile file = new PatientFile()
            {
                Age = 20,
                DateOfEnd = new DateTime(2023, 1, 1),
                DateOfRegister = DateTime.Now,
                DescriptionComplaintsGlobal = "This is a test",
                DiagnosisDescription = "This is a string",
                DiagnosisNumber = "1000",
                HeadPractitioner = phy,
                HeadPractitionerId = 1,
                Id = 1,
                IntakeBy = phy,
                IntakeById = 1,
                SupervisionBy = null,
                SupervisionById = null,
                Patient = patient,
                PatientId = 1,
                Remarks = new List<Remark>(),
                Treatments = new List<Treatment>(),
                Title = "Diren's file"
            };
            Operation operation = new Operation()
            {
                Description = "This is description",
                MandatoryExplanation = true,
                Value = "1000"
            };

            Treatment treatment = new Treatment()
            {
                DateCreated = new DateTime(2022, 1, 22),
                DateOfTreatment = new DateTime(3000, 5, 22),
                Description = "This is description",
                Id = 1,
                Particularities = null,
                PatientFile = file,
                PatientFileId = 1,
                Physiotherapist = phy,
                PhysiotherapistId = 1,
                Room = "Testruimte",
                Type = operation,
                TypeId = "1000"
            };

            Treatment treatment2 = new Treatment()
            {
                DateCreated = new DateTime(2022, 1, 22),
                DateOfTreatment = new DateTime(2022, 5, 22),
                Description = "This is description",
                Id = 1,
                Particularities = null,
                PatientFile = file,
                PatientFileId = 1,
                Physiotherapist = phy,
                PhysiotherapistId = 1,
                Room = "Testruimte",
                Type = operation,
                TypeId = "1000"
            };

            //Act
            //After the date of firing
            bool invalid =
                patientValidation.ReturnTrueWhenDateIsOutsideRegistration(file, treatment);
            //Within the two dates given to patient
            bool invalid2 =
                patientValidation.ReturnTrueWhenDateIsOutsideRegistration(file, treatment2);

            //Assert
            Assert.True(invalid);
            Assert.False(invalid2);
        }

        [Fact]
        public void BR_4()
        {
            Physiotherapist phy = new Physiotherapist()
            {
                AvailabilityEnd = new DateTime(1, 1, 1, 8, 0, 0),
                AvailabilityStart = new DateTime(1, 1, 1, 20, 0, 0),
                BigId = 1234,
                Email = "abc@abcd.nl",
                EmployeeId = 5,
                Id = 1,
                IsIntern = false,
                Name = "Diren",
                Phone = "0612345678"
            };

            Patient patient = new Patient()
            {
                Birthday = new DateTime(2001, 4, 4),
                Email = "diren@hotmail.com",
                Gender = "Male",
                Name = "Diren",
                Occupation = "Student",
                OccupationId = 2158837,
                PatientId = 1,
                Phone = "0612345678",
                Picture = null,
                PictureFormat = null
            };

            PatientFile file = new PatientFile()
            {
                Age = 20,
                DateOfEnd = new DateTime(2023, 1, 1),
                DateOfRegister = DateTime.Now,
                DescriptionComplaintsGlobal = "This is a test",
                DiagnosisDescription = "This is a string",
                DiagnosisNumber = "1000",
                HeadPractitioner = phy,
                HeadPractitionerId = 1,
                Id = 1,
                IntakeBy = phy,
                IntakeById = 1,
                SupervisionBy = null,
                SupervisionById = null,
                Patient = patient,
                PatientId = 1,
                Remarks = new List<Remark>(),
                Treatments = new List<Treatment>(),
                Title = "Diren's file"
            };
            Operation operation = new Operation()
            {
                Description = "This is description",
                MandatoryExplanation = true,
                Value = "1000"
            };

            Treatment treatment = new Treatment()
            {
                DateCreated = new DateTime(2022, 1, 22),
                DateOfTreatment = new DateTime(2022, 5, 22),
                Description = "This is description",
                Id = 1,
                Particularities = null,
                PatientFile = file,
                PatientFileId = 1,
                Physiotherapist = phy,
                PhysiotherapistId = 1,
                Room = "Testruimte",
                Type = operation,
                TypeId = "1000"
            };

            Treatment treatment2 = new Treatment()
            {
                DateCreated = new DateTime(2022, 1, 22),
                DateOfTreatment = new DateTime(2022, 5, 22),
                Description = "This is description",
                Id = 1,
                Particularities = "advb",
                PatientFile = file,
                PatientFileId = 1,
                Physiotherapist = phy,
                PhysiotherapistId = 1,
                Room = "Testruimte",
                Type = operation,
                TypeId = "1000"
            };

            //Act
            //Er zit geen string in particularities en het is verplicht.
            bool invalid =
                patientValidation
                    .ReturnTrueWhenParticularitiesIsEmptyWithMandatoryExplanationTrue(operation, treatment);

            //Er zit string in particularities en het is verplicht.
            bool invalid2 =
                patientValidation
                    .ReturnTrueWhenParticularitiesIsEmptyWithMandatoryExplanationTrue(operation, treatment2);

            //Assert
            Assert.True(invalid);
            Assert.False(invalid2);
        }


        [Fact]
        public void BR_5()
        {
            //arrange
            DateTime birthday = DateTime.Now;
            DateTime birthday2 = DateTime.Now.AddYears(-17);
            //act
            bool isOldEnough = ageAnnotions.IsValid(birthday);
            bool isOldEnough2 = ageAnnotions.IsValid(birthday2);
            //assert

            //Is niet oud genoeg
            Assert.False(isOldEnough);
            //Is oud genoeg
            Assert.True(isOldEnough2);
        }

        [Fact]
        public void BR_6()
        {
            //arrange
            Physiotherapist phy = new Physiotherapist()
            {
                AvailabilityEnd = new DateTime(1, 1, 1, 8, 0, 0),
                AvailabilityStart = new DateTime(1, 1, 1, 20, 0, 0),
                BigId = 1234,
                Email = "abc@abcd.nl",
                EmployeeId = 5,
                Id = 1,
                IsIntern = false,
                Name = "Diren",
                Phone = "0612345678"
            };

            Patient patient = new Patient()
            {
                Birthday = new DateTime(2001, 4, 4),
                Email = "diren@hotmail.com",
                Gender = "Male",
                Name = "Diren",
                Occupation = "Student",
                OccupationId = 2158837,
                PatientId = 1,
                Phone = "0612345678",
                Picture = null,
                PictureFormat = null
            };

            PatientFile file = new PatientFile()
            {
                Age = 20,
                DateOfEnd = new DateTime(2023, 1, 1),
                DateOfRegister = DateTime.Now,
                DescriptionComplaintsGlobal = "This is a test",
                DiagnosisDescription = "This is a string",
                DiagnosisNumber = "1000",
                HeadPractitioner = phy,
                HeadPractitionerId = 1,
                Id = 1,
                IntakeBy = phy,
                IntakeById = 1,
                SupervisionBy = null,
                SupervisionById = null,
                Patient = patient,
                PatientId = 1,
                Remarks = new List<Remark>(),
                Treatments = new List<Treatment>(),
                Title = "Diren's file"
            };

            TreatmentPlan treatmentPlan = new TreatmentPlan()
            {
                Id = 1,
                Duration = 30,
                File = file,
                FileId = 1,
                MaxSessions = 1,
                Physiotherapist = phy,
                PhysiotherapistId = 1,
                Title = "A Plan With Diren"
            };

            Session session1 = new Session()
            {
                AppointmentBegin = new DateTime(2022, 5, 5, 15, 30, 0),
                AppointmentEnd = new DateTime(2022, 5, 5, 16, 0, 0),
                AppointmentMade = DateTime.Now,
                HeadPhysiotherapist = phy,
                HeadPhysiotherapistId = 1,
                Id = 1,
                Patient = patient,
                PatientId = 1,
                TreatmentPlan = treatmentPlan,
                TreatmentPlanId = 1,
            };

            Session session2 = new Session()
            {
                AppointmentBegin = DateTime.Now.AddHours(12),
                AppointmentEnd = DateTime.Now.AddHours(13),
                AppointmentMade = DateTime.Now,
                HeadPhysiotherapist = phy,
                HeadPhysiotherapistId = 1,
                Id = 1,
                Patient = patient,
                PatientId = 1,
                TreatmentPlan = treatmentPlan,
                TreatmentPlanId = 1,
            };

            //act
            bool invalid1 = appointmentValidation.CheckHoursBeforeAppointmentDeleteIsNotTwentyFour(session1);
            bool invalid2 = appointmentValidation.CheckHoursBeforeAppointmentDeleteIsNotTwentyFour(session2);

            //assert
            //Geeft error weer
            Assert.True(invalid2);
            //Geeft geen error weer
            Assert.False(invalid1);
        }
    }
}

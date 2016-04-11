using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace DIO.ServiceTest
{
    [TestClass]
    public class TaoControllerTest
    {
        #region Create
        [TestMethod]
        public void CreateTaoSuccessTest()
        {
            Core.MockRepo.MockTao mockTao = new Core.MockRepo.MockTao();
            Service.Controllers.TaoController ctrlr = new Service.Controllers.TaoController(mockTao);
            IHttpActionResult res = ctrlr.CreateTao(new Core.Models.Tao { FirstName = "test4", LastName = "test4", MiddleName = "test4" });
            Assert.AreEqual(typeof(OkNegotiatedContentResult<int>), res.GetType());
        }

        [TestMethod]
        public void CreateTaoExceptionTest()
        {
            Core.MockRepo.MockTao mockTao = new Core.MockRepo.MockTao();
            mockTao.ExceptionToThrow = new Exception("Test Exception");
            Service.Controllers.TaoController ctrlr = new Service.Controllers.TaoController(mockTao);
            IHttpActionResult res = ctrlr.CreateTao(new Core.Models.Tao { FirstName = "test4", LastName = "test4", MiddleName = "test4" });
            Assert.AreEqual(typeof(ExceptionResult), res.GetType());
        }
        #endregion

        #region Read
        [TestMethod]
        public void GetTaoSuccessTest()
        {
            Core.MockRepo.MockTao mockTao = new Core.MockRepo.MockTao();
            Service.Controllers.TaoController ctrlr = new Service.Controllers.TaoController(mockTao);
            IHttpActionResult res = ctrlr.GetTao(1);
            Assert.AreEqual(typeof(OkNegotiatedContentResult<Core.Models.Tao>), res.GetType());
        }

        [TestMethod]
        public void GetTaoNotFoundTest()
        {
            Core.MockRepo.MockTao mockTao = new Core.MockRepo.MockTao();
            Service.Controllers.TaoController ctrlr = new Service.Controllers.TaoController(mockTao);
            IHttpActionResult res = ctrlr.GetTao(5);
            Assert.AreEqual(typeof(NotFoundResult), res.GetType());
        }

        [TestMethod]
        public void GetTaoExceptionTest()
        {
            Core.MockRepo.MockTao mockTao = new Core.MockRepo.MockTao();
            mockTao.ExceptionToThrow = new Exception("Test Exception");
            Service.Controllers.TaoController ctrlr = new Service.Controllers.TaoController(mockTao);
            IHttpActionResult res = ctrlr.GetTao(5);
            Assert.AreEqual(typeof(ExceptionResult), res.GetType());
        }

        [TestMethod]
        public void GetAllTaoTest()
        {
            Core.MockRepo.MockTao mockTao = new Core.MockRepo.MockTao();
            Service.Controllers.TaoController ctrlr = new Service.Controllers.TaoController(mockTao);
            List<Core.Models.Tao> res = ctrlr.GetAllTaos();
            Assert.AreEqual(3, res.Count);
        }
        #endregion

        #region Update
        [TestMethod]
        public void UpdateTaoSuccessTest()
        {
            Core.MockRepo.MockTao mockTao = new Core.MockRepo.MockTao();
            Service.Controllers.TaoController ctrlr = new Service.Controllers.TaoController(mockTao);
            IHttpActionResult res = ctrlr.UpdateTao(new Core.Models.Tao { ID = 2, FirstName = "test4", LastName = "test4", MiddleName = "test4" });
            Assert.AreEqual(typeof(OkResult), res.GetType());
        }

        [TestMethod]
        public void UpdateTaoNotFoundTest()
        {
            Core.MockRepo.MockTao mockTao = new Core.MockRepo.MockTao();
            Service.Controllers.TaoController ctrlr = new Service.Controllers.TaoController(mockTao);
            IHttpActionResult res = ctrlr.UpdateTao(new Core.Models.Tao { ID = 5, FirstName = "test4", LastName = "test4", MiddleName = "test4" });
            Assert.AreEqual(typeof(NotFoundResult), res.GetType());
        }

        [TestMethod]
        public void UpdateTaoExceptionTest()
        {
            Core.MockRepo.MockTao mockTao = new Core.MockRepo.MockTao();
            mockTao.ExceptionToThrow = new Exception("Test Excpetion");
            Service.Controllers.TaoController ctrlr = new Service.Controllers.TaoController(mockTao);
            IHttpActionResult res = ctrlr.UpdateTao(new Core.Models.Tao { ID = 5, FirstName = "test4", LastName = "test4", MiddleName = "test4" });
            Assert.AreEqual(typeof(ExceptionResult), res.GetType());
        }
        #endregion

        #region Delete

        #endregion
    }
}

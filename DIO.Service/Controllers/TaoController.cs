using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DIO.Service.Controllers
{
    public class TaoController : ApiController
    {
        private Core.Interfaces.ITao _taoRepo;
        public TaoController()
        {
            _taoRepo = new Core.BL.Tao();
        }

        public TaoController(Core.Interfaces.ITao repo)
        {
            _taoRepo = repo;
        }

        public List<Core.Models.Tao> GetAllTaos()
        {
            return _taoRepo.Read();
        }

        public IHttpActionResult GetTao(int id)
        {
            try
            {
                var tao = _taoRepo.Read(id);
                if (tao == null)
                {
                    return NotFound();
                }
                return Ok(tao);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult CreateTao(Core.Models.Tao tao)
        {
            try
            {
                int identity = _taoRepo.Create(tao);
                return Ok(identity);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult UpdateTao(Core.Models.Tao tao)
        {
            try
            {
                if (_taoRepo.Update(tao))
                    return Ok();
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

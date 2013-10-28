using CheckList.DAL.Repository;
using CheckList.Domain;
using CheckList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace CheckList.Controllers
{
    [Authorize]
    public class ChecksController : ApiController
    {
        private readonly Repository _repository;
        private readonly CheckRepository _checkRepo;

        public ChecksController()
        {
            _repository = new Repository(MvcApplication.UnitOfWork.Session);
            _checkRepo = new CheckRepository(MvcApplication.UnitOfWork.Session);
        }

        public IEnumerable<CheckModel> Get(string q = "", string systemQuery = "", string isImplementedQuery = "", string typeQuery = "", bool desc = false,
                                                        int? limit = null, int offset = 0)
        {
            bool? isImplemented = null;
            if (isImplementedQuery != null && isImplementedQuery.ToUpper().Equals("1"))
                isImplemented = true;
            else
                if (isImplementedQuery != null && isImplementedQuery.ToUpper().Equals("0"))
                    isImplemented = false;

            IEnumerable<Check> checks = _checkRepo.GetChecks(q, systemQuery,typeQuery, isImplemented, limit, offset);
            List<CheckModel> result = new List<CheckModel>();
            foreach (Check check in checks)
            {
                result.Add(new CheckModel(check));
            }
            return result;
        }

        public CheckModel Get(int id)
        {
            Check check = _repository.Get<Check>(id);
            CheckModel model = new CheckModel(check);
            return model;
        }

        public IEnumerable<OfpSystem> GetSystems()
        {
            return _repository.GetAll<OfpSystem>();
        }

        public IEnumerable<CheckType> GetTypes()
        {
            return _repository.GetAll<CheckType>();
        }

        public HttpResponseMessage Post([FromBody]CheckModel model)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                Check check = ModelToCheck(model);
                _repository.Save<Check>(check);
                MvcApplication.UnitOfWork.Commit();
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                message = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Critical Post Exception"
                };
            }
            return message;
        }

        public HttpResponseMessage Put(int id, [FromBody]CheckModel model)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                Check check = ModelToCheck(model);
                check.Id = id;
                _repository.SaveOrUpdate<Check>(check);
                MvcApplication.UnitOfWork.Commit();
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                message = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Critical Post Exception"
                };
            }
            return message;
        }

        public void Delete(int id)
        {
            try
            {
                Check toBeDeleted = _repository.Get<Check>(id);
                _repository.Delete<Check>(toBeDeleted);
                MvcApplication.UnitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Critical Delete Exception"
                });
            }
        }

        private Check ModelToCheck(CheckModel model)
        {
            Check check = new Check();
            check.Description = model.Description;
            check.IsImplemented = (model.IsImplemented == 1) ? true : false;
            OfpSystem ofpSystem = _repository.Find<OfpSystem>(x => x.Label == model.OfpSystem).SingleOrDefault();
            CheckType checkType = _repository.Find<CheckType>(x => x.Label == model.Type).SingleOrDefault();
            check.OfpSystem = ofpSystem;
            check.Type = checkType;
            return check;
        }
    }
}

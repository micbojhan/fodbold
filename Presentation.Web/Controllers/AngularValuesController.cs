using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Core.DomainModel.Model;
using Core.DomainServices;

namespace Presentation.Web.Controllers
{
    public class AngularValuesController : ApiController
    {
        private readonly IGenericRepository<Test> _testRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public AngularValuesController(IGenericRepository<Test> testRepository, IUnitOfWork unitOfWork)
        {
            _testRepository = testRepository;
            _unitOfWork = unitOfWork;
        }

        // GET api/values
        public IEnumerable<Test> Get()
        {
            var list = _testRepository.AsQueryable().ToList();
            return list;
        }

        // GET api/values/5
        public Test Get(int id)
        {
            var selected = _testRepository.GetByKey(id);
            return selected;
        }

        // POST api/values
        public Test Post([FromBody]string value)
        {
            var addedTest = _testRepository.Insert(new Test { Name = value });
            _unitOfWork.Save();
            return addedTest;

        }

        // PUT api/values/5
        public Test Put(int id, [FromBody]string value)
        {
            //var addedTest = db.Tests.Add(new Test { Name = value });
            _unitOfWork.Save();
            return new Test();

        }

        // DELETE api/values/5
        public Test Delete(int id)
        {
            var deleted = _testRepository.DeleteByKey(id);
            _unitOfWork.Save();
            return deleted;
        }
    }
}

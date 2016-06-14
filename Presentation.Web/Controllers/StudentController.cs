﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Core.DomainModel;
using Core.DomainModel.OldModel;
using Core.DomainServices;
using Microsoft.AspNet.Identity;
using Presentation.Web.Mail;
using Presentation.Web.Models.Student;

namespace Presentation.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IIdentityMessageService _emailService;
        private readonly IMailHandler _mailHandler;
        private readonly IUnitOfWork _unitOfWork;

        // Hardcoded pagingsize
        private const int PageSize = 3;

        /// <summary>
        /// The constructor takes GenericRepositories as argument, which is automatically created by ninject in NinjectWebCommon.
        /// This enables us to quickly get references to our context, by simply typing, for
        /// example, "IGenericRepository<Course> courseRepository", which we can use right away.
        /// Whenever a changes is made, use _unitOfWork.Save() to save any changes.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="studentRepository"></param>
        /// <param name="emailService"></param>
        /// <param name="mailHandler"></param>
        public StudentController(IUnitOfWork unitOfWork, IGenericRepository<Student> studentRepository, IIdentityMessageService emailService, IMailHandler mailHandler)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
            _mailHandler = mailHandler;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Students are here.";

            // We're putting the SelectListItems into a ViewBag, because we do not need to send it back.
            // We only send the selectedId back.
            // Example of a dropdown menu.
            ViewBag.StudentIds = _studentRepository.AsQueryable().Select(s =>
                new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Id.ToString()
                });

            // Example of paging students in a table.
            var model = new IndexViewModel()
            {
                PagedStudents =
                    new PagedData<NewStudentViewModel>()
                    {
                        // Always configure automapper into mapping viewmodels against domainmodels.
                        Data = Mapper.Map<List<Student>, List<NewStudentViewModel>>(_studentRepository.AsQueryable().OrderBy(p => p.Name).Take(PageSize).ToList()).ToList(),
                        NumberOfPages = PagingsSizeHelper()
                    }
            };
            return View(model);
        }

        private int PagingsSizeHelper()
        {
            return Convert.ToInt32(Math.Ceiling((double) _studentRepository.Count()/PageSize));
        }

        public ActionResult _Students(int page)
        {
            var model = new IndexViewModel()
            {
                PagedStudents = new PagedData<NewStudentViewModel>()
                {
                    // Always configure automapper into mapping viewmodels against domainmodels.
                    Data = Mapper.Map<List<Student>, List<NewStudentViewModel>>(_studentRepository.AsQueryable().OrderBy(p => p.Name).Skip(PageSize * (page - 1)).Take(PageSize).ToList()),
                    NumberOfPages = PagingsSizeHelper()
                }
            };
            return PartialView(model);
        }

        public ActionResult NewStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewStudent(NewStudentViewModel model)
        {
            // Configure automapper into mapping viewmodels against domainmodels.
            // This can also be done conversely.
            var student = Mapper.Map<Student>(model);
            student.CreatedOn = DateTime.Now;
            student.ModifiedOn = DateTime.Now;
            _studentRepository.Insert(student);

            _unitOfWork.Save();
            return View(model);
        }

        /// <summary>
        /// Functions used in UnitTest Examples - How to test a controller (FakeDbContext).
        /// </summary>
        /// <returns></returns>
        public List<NewStudentViewModel> IndexStudentsById()
        {
            return Mapper.Map<List<Student>, List<NewStudentViewModel>>(_studentRepository.AsQueryable().OrderBy(p => p.Id).ToList());
        }

        public List<NewStudentViewModel> IndexStudentsByName()
        {
            return Mapper.Map<List<Student>, List<NewStudentViewModel>>(_studentRepository.AsQueryable().OrderBy(p => p.Name).ToList());
        }

        /// <summary>
        /// Function used to find a student by id.
        /// Will return a json object which can be used in javascript.
        /// </summary>
        /// <param name="id">Id specifying the student</param>
        /// <returns>Json string resembling a studentviewmodel</returns>
        public ActionResult FindStudent(int? id)
        {
            if (id == null)
                return Json(new NewStudentViewModel(){ Name = "null" }, JsonRequestBehavior.AllowGet);

            var student = _studentRepository.AsQueryable().SingleOrDefault(x => x.Id == id);

            if (student == null)
                return HttpNotFound();

            var viewmodel = Mapper.Map<NewStudentViewModel>(student);

            return Json(viewmodel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Mail()
        {
            return View();
        }

        public async Task<ActionResult> SendMail(MailViewModel model)
        {
            var message = new IdentityMessage
            {
                Body = _mailHandler.GetMailMessage(model, "EmailTemplate.cshtml"),
                Destination = model.Email,
                Subject = model.Subject
            };

            await _emailService.SendAsync(message);
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
    }
}

using BigSchool.DTOs;
using BigSchool.FileModels;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace BigSchool.Controllers
{
    // Authorize
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbcontext;
        public AttendancesController()
        {
            _dbcontext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto AttendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbcontext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == AttendanceDto.CourseId))

                return BadRequest("The Attendance already exists!");

            var attendance = new Attendance
            {
                CourseId = AttendanceDto.CourseId,
                AttendeeId = userId
            };
            _dbcontext.Attendances.Add(attendance);
            _dbcontext.SaveChanges();
            return Ok();
        }

    }
}

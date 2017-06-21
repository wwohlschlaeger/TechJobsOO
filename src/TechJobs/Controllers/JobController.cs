using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {
		 
        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();

        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
			// TODO #1 - Completted
			// get the Job with the given ID and pass it into the view
			SearchJobsViewModel jobsByIdViewModel = new SearchJobsViewModel();
			//JobFieldData<Employer> jobFieldData = new JobFieldData<Employer>() ;
			//Job jobById= jobData.Find(id);
			//jobsByIdViewModel.Jobs[0] = jobFieldData.Find(id);
			jobsByIdViewModel.Jobs = new List<Job>();  //  Kit recommended instantiating the .Jobs so I can pass in the .Find
			jobsByIdViewModel.Jobs.Add(jobData.Find(id));  // Kit recommended .Add
			//Console.WriteLine(jobsByIdViewModel.Jobs[0]);
			//Console.ReadLine();
			jobsByIdViewModel.Title = "Job #" + id;
			
            return View(jobsByIdViewModel);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
			SearchJobsViewModel jobsByIdViewModel = new SearchJobsViewModel();
			jobsByIdViewModel.Jobs = jobData.Jobs;
			return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
			// TODO #6 - Validate the ViewModel and if valid, create a 
			// new Job and add it to the JobData data store. Then
			// redirect to the Job detail (Index) action/view for the new Job.

			if (ModelState.IsValid)
			{
				Job newJob = new Job()
				{
					Name = newJobViewModel.Name,
					Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
					Location = jobData.Locations.Find(newJobViewModel.LocationID),
					CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),
					PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID),
				};
				SearchJobsViewModel jobsById = new SearchJobsViewModel();
				jobData.Jobs.Add(newJob);
				jobsById.Jobs = new List<Job>();
				jobsById.Jobs.Add(newJob);
				return View("Index", jobsById);
			}

			else
				return View(newJobViewModel);
		}
    }
}

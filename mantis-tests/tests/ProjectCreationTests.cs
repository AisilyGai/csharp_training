using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        public static IEnumerable<ProjectData> RandomDataProvider()
        {
            List<ProjectData> project = new List<ProjectData>();
            for (int i = 0; i < 3; i++)
            {
                project.Add(new ProjectData()
                {
                    Name = GenerateRandonString(30),
                });
            }
            return project;
        }

        [Test, TestCaseSource("RandomDataProvider")]
        public void ProjectCreationTest(ProjectData project)
        {
            List<ProjectData> oldProjects = app.project.GetAllFromUI();

            app.project.Creation(project);

            Thread.Sleep(3000);
            Assert.AreEqual(oldProjects.Count + 1, app.project.GetProjectCount());

            List<ProjectData> newProjects = app.project.GetAllFromUI();

            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemoveTests : AuthTestBase
    {
        [Test]
        public void ProjectRemoveTest()
        {
            app.project.ProjectElementVerification();

            List<ProjectData> oldProjects = app.project.GetAllFromUI();
            int toBeRemoved = 0;

            app.project.Remove(toBeRemoved);

            Assert.AreEqual(oldProjects.Count - 1, app.project.GetProjectCount());

            List<ProjectData> newProjects = app.project.GetAllFromUI();

            oldProjects.RemoveAt(toBeRemoved);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
namespace Paul.PhysicsSimulator.UnitTest.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Utility used in Test classes to access the file system
    /// </summary>
    public class FileSystemUtility
    {
        /// <summary>
        /// Get the path of a file included in the project and used in unitTest:
        /// - put the file in a folder in the current project setting the property to "copy always"
        /// - put the attribute in the test method [DeploymentItem(@"Folder\File.ext")]        
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static string GetFullFilePathFromRelativePath(string relativePath)
        {
            string deploymentFolder = System.IO.Path.GetDirectoryName(typeof(FileSystemUtility).Assembly.Location);
            string deployedTestData = System.IO.Path.Combine(deploymentFolder, relativePath);
            return deployedTestData;
        }
    }
}

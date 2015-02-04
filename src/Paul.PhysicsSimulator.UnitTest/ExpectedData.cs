namespace Paul.PhysicsSimulator.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Data.Common;
    using Paul.PhysicsSimulator.UnitTest.Utilities;

    /// <summary>
    /// Contains a list of expected data to use in the test methods
    /// </summary>
    public class ExpectedData
    {
        /// <summary>
        /// Get the expected data for the ParticleSpring from the file "ExpectedData\Spring simulation.xlsx"
        /// </summary>
        /// <returns></returns>
        public DataTable GetParticleSpring_Data()
        {
            DataSet data = ExcelUtility.ReadExcelFile(FileSystemUtility.GetFullFilePathFromRelativePath("ExpectedData\\Spring simulation.xlsx"));                
            return data.Tables["Ideale$"];
            
        }
    }
}

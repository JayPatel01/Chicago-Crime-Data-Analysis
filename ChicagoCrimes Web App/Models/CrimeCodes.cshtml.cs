using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Data;
  
namespace crimes.Pages  
{  
    public class CrimeCodesModel : PageModel  
    {  
        public List<Models.CrimeCodes> CrimeCodesList { get; set; }
				public Exception EX { get; set; }
  
        public void OnGet()  
        {  
				  List<Models.CrimeCodes> crimes = new List<Models.CrimeCodes>();
					
					// clear exception:
					EX = null;
					
					try
					{
						string sql = string.Format(@"
SELECT Codes.IUCR,Codes.PrimaryDesc,Codes.SecondaryDesc, COUNT(*) AS TotalNumOfCrimes
FROM Crimes
RIGHT JOIN Codes ON Crimes.IUCR = Codes.IUCR 
GROUP BY Codes.IUCR,Codes.PrimaryDesc,Codes.SecondaryDesc
ORDER BY TotalNumOfCrimes DESC;
	");

						DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

						foreach (DataRow row in ds.Tables["TABLE"].Rows)
						{
							Models.CrimeCodes c = new Models.CrimeCodes();

							c.IUCR = Convert.ToString(row["IUCR"]);
							c.PrimaryDesc = Convert.ToString(row["PrimaryDesc"]);
							c.SecondaryDesc = Convert.ToString(row["SecondaryDesc"]);
							c.TotalNumOfCrimes = Convert.ToInt32(row["TotalNumOfCrimes"]);

							crimes.Add(c);
						}
					}
					catch(Exception ex)
					{
					  EX = ex;
					}
					finally
					{
            CrimeCodesList = crimes;  
				  }
        }  
				
    }//class
}//namespace
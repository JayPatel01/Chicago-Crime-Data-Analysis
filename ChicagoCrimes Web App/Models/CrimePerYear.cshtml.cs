using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Data;
  
namespace crimes.Pages  
{  
    public class CrimePerYearModel : PageModel  
    {  
        public List<Models.CrimePerYear> CrimePerYearList { get; set; }
		  public Exception EX { get; set; }
        public List<int> Year;
        public List<int> TotalNumOfCrimes;
      
  
        public void OnGet()  
        {  
				  List<Models.CrimePerYear> crimes = new List<Models.CrimePerYear>();
					
					// clear exception:
					EX = null;
               Year = new List<int>();
               TotalNumOfCrimes = new List<int>();
					
					try
					{
						string sql = string.Format(@"
SELECT Crimes.Year, COUNT(*) AS TotalNumOfCrimes
FROM Crimes
GROUP BY Crimes.Year
ORDER BY Year;
	");

						DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

						foreach (DataRow row in ds.Tables["TABLE"].Rows)
						{
							//Models.CrimeCodes c = new Models.CrimeCodes();
							int year = 0;
                     int totalNumOfCrimes = 0;

							year = Convert.ToInt32(row["Year"]);
							totalNumOfCrimes = Convert.ToInt32(row["TotalNumOfCrimes"]);

							Year.Add(year);
                     TotalNumOfCrimes.Add(totalNumOfCrimes);
						}
					}
					catch(Exception ex)
					{
					  EX = ex;
					}
					finally
					{
            //CrimePerYearList = crimes;  
				  }
        }  
				
    }//class
}//namespace
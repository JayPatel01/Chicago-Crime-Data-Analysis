using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Data;
  
namespace crimes.Pages  
{  
    public class CrimesTop10Model : PageModel  
    {  
        public List<Models.Crime> CrimeList { get; set; }
				public Exception EX { get; set; }
  
        public void OnGet()  
        {  
				  List<Models.Crime> crimes = new List<Models.Crime>();
					
					// clear exception:
					EX = null;
					
					try
					{
						string sql = string.Format(@"
SELECT top 10 Codes.IUCR,Codes.PrimaryDesc,Codes.SecondaryDesc, COUNT(*) AS TotalNumOfCrimes, ROUND((CONVERT(float,COUNT(*))*100.0/(SELECT COUNT(*) FROM Crimes)),2) AS percentOfCrime,ROUND(AVG(CONVERT(float,Arrested))*100.0,2) AS PercentageOfArrest
FROM Crimes
INNER JOIN Codes ON Crimes.IUCR = Codes.IUCR
GROUP BY Codes.IUCR,Codes.PrimaryDesc,Codes.SecondaryDesc
ORDER BY TotalNumOfCrimes DESC;
	");

						DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

						foreach (DataRow row in ds.Tables["TABLE"].Rows)
						{
							Models.Crime c = new Models.Crime();

							c.IUCR = Convert.ToString(row["IUCR"]);
							c.PrimaryDesc = Convert.ToString(row["PrimaryDesc"])+" "+ Convert.ToString(row["SecondaryDesc"]);;
							//c.SecondaryDesc = Convert.ToString(row["SecondaryDesc"]);
							c.TotalNumOfCrimes = Convert.ToInt32(row["TotalNumOfCrimes"]);
							c.PercentOfCrime = Convert.ToDouble(row["PercentOfCrime"]);
                     c.PercentageOfArrest = Convert.ToDouble(row["PercentageOfArrest"]);

							crimes.Add(c);
						}
					}
					catch(Exception ex)
					{
					  EX = ex;
					}
					finally
					{
            CrimeList = crimes;  
				  }
        }  
				
    }//class
}//namespace
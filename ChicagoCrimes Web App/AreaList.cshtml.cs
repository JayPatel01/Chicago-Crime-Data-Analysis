using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Data;
  
namespace crimes.Pages  
{  
    public class AreaListModel : PageModel  
    {  
        public List<Models.AreaList> AreaListList { get; set; }
				public Exception EX { get; set; }
  
        public void OnGet()  
        {  
				  List<Models.AreaList> crimes = new List<Models.AreaList>();
					
					// clear exception:
					EX = null;
					
					try
					{
						string sql = string.Format(@"
SELECT Areas.Area,Areas.AreaName, COUNT(*) AS TotalNumOfCrimes, ROUND((CONVERT(float,COUNT(*))*100.0/(SELECT COUNT(*) FROM Crimes)),2) AS percentOfCrime
FROM Crimes
INNER JOIN Areas ON Crimes.Area = Areas.Area
GROUP BY Areas.Area,Areas.AreaName
ORDER BY Areas.AreaName;
	");

						DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

						foreach (DataRow row in ds.Tables["TABLE"].Rows)
						{
							Models.AreaList c = new Models.AreaList();

							c.AreaNumber = Convert.ToInt32(row["Area"]);
							c.AreaName = Convert.ToString(row["AreaName"]);
							c.TotalNumOfCrimes = Convert.ToInt32(row["TotalNumOfCrimes"]);
                     c.PercentOfCrime = Convert.ToDouble(row["percentOfCrime"]);

							crimes.Add(c);
						}
					}
					catch(Exception ex)
					{
					  EX = ex;
					}
					finally
					{
            AreaListList = crimes;  
				  }
        }  
				
    }//class
}//namespace
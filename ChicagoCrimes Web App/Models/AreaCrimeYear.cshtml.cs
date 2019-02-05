using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Data;
  
namespace crimes.Pages  
{  
    public class AreaCrimeYearModel : PageModel  
    {  
           public List<int> Year;
           public List<int> TotalNumOfCrimes;
           public Exception EX { get; set; }
        
   
  
        public void OnGet(string input)  
        {  
				  Year  = new List<int>();
              TotalNumOfCrimes  = new List<int>();
					
                    
                    // make input available to web page:
				//	Input = input;
                    
                    
					// clear exception:
					EX = null;
					
					try
					{
                    //
						// Do we have an input argument?  If so, we do a lookup:
						//
						if (input == null)
						{
							//
							// there's no page argument, perhaps user surfed to the page directly?  
							// In this case, nothing to do.
							//
						}
						else  
						{
							// 
							// Lookup movie(s) based on input, which could be id or a partial name:
							// 
							int id;
							string sql;
 
  

							if (System.Int32.TryParse(input, out id))
							{
								// lookup movie by movie id:
								sql = string.Format(@"
SELECT Crimes.Year, Count(Crimes.Year) AS TotalNumOfCrimes
FROM Crimes
INNER JOIN Areas ON Crimes.Area= Areas.Area
WHERE Areas.Area = '{0}'
GROUP BY Year
ORDER BY Year;
	", id);

    
    
							}
							else
							{
								// lookup movie(s) by partial name match:
								input = input.Replace("'", "''");
	sql = string.Format(@"
SELECT Crimes.Year, Count(Crimes.Year) AS TotalNumOfCrimes
FROM Crimes
INNER JOIN Areas ON Crimes.Area= Areas.Area
WHERE Areas.AreaName LIKE '%{0}%'
GROUP BY Year
ORDER BY Year;
	", input);
    
    

							}
                            
                        DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);
        
                        int i=0;
            
						foreach (DataRow row in ds.Tables["TABLE"].Rows)
						{
							int year = 0;
                     int totalNumOfCrimes = 0;
                     year= Convert.ToInt32(row["Year"]);          
                     totalNumOfCrimes = Convert.ToInt32(row["TotalNumOfCrimes"]);     
                     Year.Add( year);
                     TotalNumOfCrimes.Add(totalNumOfCrimes);

                     i++;
						}
					}
                        
					}
					catch(Exception ex)
					{
					  EX = ex;
					}
					finally
					{
        
				  }
        }   
				
    }//class
}//namespace
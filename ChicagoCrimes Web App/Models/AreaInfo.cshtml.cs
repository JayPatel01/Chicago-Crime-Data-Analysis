using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Data;
  
namespace crimes.Pages  
{  
    public class AreaTop10Model : PageModel  
    {  
				public List<Models.Crime> CrimeList { get; set; }
				public string Input { get; set; }
				public int NumCrimes { get; set; }
				public Exception EX { get; set; }
            public string AreaName {get;set;}
            public int AreaNumber {get;set;}
  
        public void OnGet(string input)  
        {  
				  List<Models.Crime> crimes = new List<Models.Crime>();
					
					// make input available to web page:
					Input = input;
					
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
                     string sql1 = null;
                    // string sql2 = null;

							if (System.Int32.TryParse(input, out id))
							{
								// lookup movie by movie id:
								sql = string.Format(@"
SELECT top 10 Codes.IUCR,Codes.PrimaryDesc,Codes.SecondaryDesc, COUNT(*) AS TotalNumOfCrimes, ROUND((CONVERT(float,COUNT(*))*100.0/(SELECT COUNT(*) FROM Crimes)),2) AS percentOfCrime,ROUND(AVG(CONVERT(float,Arrested))*100.0,2) AS PercentageOfArrest
FROM Crimes
INNER JOIN Codes ON Crimes.IUCR = Codes.IUCR 
WHERE Area = {0}
GROUP BY Codes.IUCR,Codes.PrimaryDesc,Codes.SecondaryDesc
ORDER BY TotalNumOfCrimes DESC;
	", id);
   
                     sql1 = string.Format(@"
                     SELECT Area, AreaName FROM Areas
WHERE Area = {0};",id);
							}
							else
							{
								// lookup movie(s) by partial name match:
								input = input.Replace("'", "''");

								sql = string.Format(@"
SELECT top 10 Codes.IUCR,Codes.PrimaryDesc,Codes.SecondaryDesc, COUNT(*) AS TotalNumOfCrimes, ROUND((CONVERT(float,COUNT(*))*100.0/(SELECT COUNT(*) FROM Crimes)),2) AS percentOfCrime,ROUND(AVG(CONVERT(float,Arrested))*100.0,2) AS PercentageOfArrest
FROM Crimes
INNER JOIN Codes ON Crimes.IUCR = Codes.IUCR
INNER JOIN Areas ON Crimes.Area = Areas.Area
WHERE AreaName LIKE '%{0}%'
GROUP BY Codes.IUCR,Codes.PrimaryDesc,Codes.SecondaryDesc
ORDER BY TotalNumOfCrimes DESC;
	", input);
   
                     sql1 = string.Format(@"
                     SELECT Area, AreaName FROM Areas
WHERE AreaName LIKE '%{0}%';",input);
   
							}

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
                     
                     DataSet ds1 = DataAccessTier.DB.ExecuteNonScalarQuery(sql1);
                     
                     foreach (DataRow row1 in ds1.Tables["TABLE"].Rows)
							{
								
                     AreaNumber = Convert.ToInt32(row1["Area"]);
                     AreaName = Convert.ToString(row1["AreaName"]);

							}
						}//else
					}
					catch(Exception ex)
					{
					  EX = ex;
					}
					finally
					{
					  CrimeList = crimes;
					  NumCrimes = crimes.Count;
				  }
				}
			
    }//class  
}//namespace
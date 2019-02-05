//
// One crime
//

namespace crimes.Models
{

  public class Crime
	{
	
		// data members with auto-generated getters and setters:
	   public string IUCR { get; set; }
		public string PrimaryDesc { get; set; }
		public string AreaName { get; set; }
      public int AreaNumber { get; set; }
		public int TotalNumOfCrimes { get; set; }
		public double PercentOfCrime { get; set; }
      public double PercentageOfArrest { get; set; }
	
		// default constructor:
		public Crime()
      {}
     
		public Crime(string Area1, int AreaNum)
		{ 
         AreaName = Area1;
         AreaNumber = AreaNum;
      }
		
		// constructor:
		public Crime(string IUCR, string PrimaryDesc, int TotalNumOfCrimes,double PercentOfCrime, double PercentageOfArrest)
		{
			IUCR = this.IUCR;
			PrimaryDesc = this.PrimaryDesc;
			//SecondaryDesc = this.SecondaryDesc;
			TotalNumOfCrimes = this.TotalNumOfCrimes;
         PercentOfCrime = this.PercentOfCrime;
			PercentageOfArrest = this.PercentageOfArrest;
		}
		
	}//class

}//namespace
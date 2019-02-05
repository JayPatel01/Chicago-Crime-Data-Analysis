//
// One crime
//

namespace crimes.Models
{

  public class AreaList
	{
	
		// data members with auto-generated getters and setters:
	   
		public string AreaName { get; set; }
      public int AreaNumber { get; set; }
		public int TotalNumOfCrimes { get; set; }
		public double PercentOfCrime { get; set; }
      
	
		// default constructor:
		public AreaList()
      {}
     
		
		
		// constructor:
		public AreaList(int AreaNumber,string AreaName, int TotalNumOfCrimes,double PercentOfCrime)
		{
			AreaNumber = this.AreaNumber;
         AreaName = this.AreaName;
			TotalNumOfCrimes = this.TotalNumOfCrimes;
         PercentOfCrime = this.PercentOfCrime;	
		}
		
	}//class

}//namespace
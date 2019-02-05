//
// One crime
//

namespace crimes.Models
{

  public class CrimePerYear
	{
	
		// data members with auto-generated getters and setters:
	    public int Year {get;set;}
		public int TotalNumOfCrimes { get; set; }
     
		
	
		// default constructor:
		public CrimePerYear()
      {}
     
		
		
		// constructor:
		public CrimePerYear(int Year, int TotalNumOfCrimes)
		{
         Year = this.TotalNumOfCrimes;
			TotalNumOfCrimes = this.TotalNumOfCrimes;
		}
		
	}//class

}//namespace
using System.ComponentModel.DataAnnotations;

namespace WhensMyMOT.Models
{
    public class Registration
    {
        [Required(ErrorMessage = ("Registration number is required"))]
        [RegularExpression("(?<Current>^[A-Za-z]{2}[0-9]{2}[A-Za-z]{3}$)|(?<Prefix>^[A-Za-z][0-9]{1,3}[A-Za-z]{3}$)|(?<Suffix>^[A-Za-z]{3}[0-9]{1,3}[A-Za-z]$)|(?<DatelessLongNumberPrefix>^[0-9]{1,4}[A-Za-z]{1,2}$)|(?<DatelessShortNumberPrefix>^[0-9]{1,3}[A-Za-z]{1,3}$)|(?<DatelessLongNumberSuffix>^[A-Za-z]{1,2}[0-9]{1,4}$)|(?<DatelessShortNumberSufix>^[A-Za-z]{1,3}[0-9]{1,3}$)|(?<DatelessNorthernIreland>^[A-Za-z]{1,3}[0-9]{1,4}$)|(?<DiplomaticPlate>^[0-9]{3}[DX]{1}[0-9]{3}$)", ErrorMessage = "Invalid UK registration number")]
        public string RegNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Colour { get; set; }

        public string ExpiryDate { get; set; }

        public string Mileage { get; set; }

        public string ErrorMessage { get; set; }
    }
}

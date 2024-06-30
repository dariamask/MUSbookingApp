
namespace BAL.Validation.Result
{
    public class Errors
    {
        //order
        public const string OrderDoesntExist = "Order doesn't exist.";

        //equipment
        public const string EquipmentDoesntExist = "Equipment doest'n exist: ";
        public const string IsNotUnique = "Equipment name is not unique.";
        public const string NotEnough = ": not enough equipment. Available quantity is: ";
    }
}
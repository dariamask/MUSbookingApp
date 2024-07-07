
using DAL.Data.Entities;

namespace BAL.Validation.Result
{
    public class Errors
    {
        //order
        public const string OrderDoesntExist = "Order doesn't exist.";

        //equipment
        public const string IsNotUnique = "Equipment name is not unique.";

        //orderline
        public static string EquipmentDoesntExist(Guid equipmentId)
        {
            return $"Equipment doest'n exist: {equipmentId}";
        }
        public static string NotEnough(Equipment equipment)
        {
            return $"Not enough equipment: {equipment.Name} - {equipment.Id}. Available quantity is: {equipment.Amount}";
        }
    }
}
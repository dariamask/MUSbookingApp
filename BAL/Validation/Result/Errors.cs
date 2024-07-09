
using DAL.Data.Entities;

namespace BAL.Validation.Result
{
    public class Errors
    {
        //order
        public const string OrderDoesntExist = "Order doesn't exist.";

        //pagination
        public const string PaginationParametersBelowZero = "Pagination parameters must be above zero.";
        public static string EmptyPages(int maxPageSize)
        {
            return $"Maximum page number is {maxPageSize}";
        }

        //equipment
        public const string IsNotUnique = "Equipment name is not unique.";

        //orderline
        public static string EquipmentDoesntExist(Guid equipmentId)
        {
            return $"Equipment with id {equipmentId} not found.";
        }
        public static string NotEnough(Equipment equipment)
        {
            return $"Not enough equipment: {equipment.Name} - {equipment.Id}. Available quantity is: {equipment.Amount}";
        }
    }
}
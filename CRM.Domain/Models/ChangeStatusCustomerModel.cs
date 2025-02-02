using CRM.Domain.Enums;

namespace CRM.Domain.Models;
public class ChangeStatusCustomerModel
{
    public long Id { get; set; }
    public CustomerStatus Status { get; set; }
}


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    public enum Status
    {
        Avaliable, //доступна

        Booked, //забронирована

        Gived, //выдана
    }
}

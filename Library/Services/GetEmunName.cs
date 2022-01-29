using Library.Domain.Entities;

namespace Library.Services
{
    public static class GetEmunName
    {
        public static string GetName(Status status)
        {
            string name = "";

            switch (status)
            {
                case Status.Avaliable: name = "Доступна"; break;;
                case Status.Booked: name = "Забронирована"; break;
                case Status.Gived: name = "Выдана"; break;
            }

            return name;
        }
    }
}

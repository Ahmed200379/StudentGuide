namespace StudentGuide.BLL.Constant
{
    public class Exceptions
    {
        public static class ExceptionMessages
        {
            public static string GetNotFoundMessage(string entity)
            {
                return $"{entity} not found.";
            }

            public static string GetAddFailedMessage(string entity)
            {
                return $"Failed to add {entity}.";
            }

            public static string GetUpdateFailedMessage(string entity)
            {
                return $"Failed to update {entity}.";
            }

            public static string GetDeleteFailedMessage(string entity)
            {
                return $"Failed to delete {entity}.";
            }

            public static string GetAlreadyExistsMessage(string entity)
            {
                return $"{entity} already exists.";
            }

        }
    }
}

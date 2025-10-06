namespace Vives.Services.Model.Extensions
{
    public static class ServiceResultExtensions
    {
        //public static ServiceResult AlreadyRemoved(this ServiceResult serviceResult)
        //{
        //    serviceResult.Messages.Add(
        //        new ServiceMessage()
        //        {
        //            Code = "AlreadyRemoved",
        //            Message = "Function was already removed",
        //            Type = ServiceMessageType.Warning
        //        });
        //    return serviceResult;
        //}

        public static T AlreadyRemoved<T>(this T serviceResult)
            where T : ServiceResult
        {
            serviceResult.Messages.Add(
                new ServiceMessage()
                {
                    Code = "AlreadyRemoved",
                    Message = "Function was already removed",
                    Type = ServiceMessageType.Warning
                });
            return serviceResult;
        }

        public static T NotFound<T>(this T serviceResult, string entityName)
            where T : ServiceResult
        {
            serviceResult.Messages.Add(
                new ServiceMessage()
                {
                    Code = "NotFound",
                    Message = $"{entityName} not found",
                    Type = ServiceMessageType.Error
                });
            return serviceResult;
        }

        //public static ServiceResult Required(this ServiceResult serviceResult, string propertyName)
        //{
        //    serviceResult.Messages.Add(
        //        new ServiceMessage()
        //        {
        //            Code = "Required",
        //            Message = $"{propertyName} is required",
        //            Type = ServiceMessageType.Error
        //        });

        //    return serviceResult;
        //}

        public static T Required<T>(this T serviceResult, string propertyName)
            where T : ServiceResult
        {
            serviceResult.Messages.Add(
                new ServiceMessage()
                {
                    Code = "Required",
                    Message = $"{propertyName} is required",
                    Type = ServiceMessageType.Error
                });

            return serviceResult;
        }
    }
}

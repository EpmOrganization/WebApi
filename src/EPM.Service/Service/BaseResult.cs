using EPM.Framework.Helper;
using EPM.Model.ApiModel;
using EPM.Model.Enum;

namespace EPM.Service.Service
{
    public class BaseResult
    {
        public ValidateResult ReturnSuccess()
        {
            return new ValidateResult()
            {
                Code = (int)CustomerCode.Success,
                Msg = EnumHelper.GetEnumDesc(CustomerCode.Success)
            };
        }

        public ValidateResult ReturnFail()
        {
            return new ValidateResult()
            {
                Code = (int)CustomerCode.Fail,
                Msg = EnumHelper.GetEnumDesc(CustomerCode.Fail)
            };
        }
    }
}

namespace Zip.Api.CustomerSummary.Domain.Entities.Communications
{
    public class MfaCode
    {
        public long Id { get; set; }
        public long ConsumerId { get; set; }
        public byte Attempt { get; set; }
        public string Code { get; set; }
        public MfaEntityType? MfaEntityType { get; set; }
        public long? EntityId { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public bool Active { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] RowVersion { get; set; }
        public bool Verified { get; set; }
        public MfaVerificationOperationType? OperationType { get; set; }
        public long? OperationId { get; set; }

        private MfaType _mfaType;
        public MfaType? MfaType
        {
            get { return _mfaType; }
            set
            {
                _mfaType = value ?? Communications.MfaType.PhoneNumber;
            }
        }

        private string _destination;
        public string Destination
        {
            get { return string.IsNullOrWhiteSpace(_destination) ? PhoneNumber : _destination; }
            set { _destination = value; }
        }
    }
}

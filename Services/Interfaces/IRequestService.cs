using CLMS.Models; // <--- kjo bën që Request të njihet

namespace CLMS.Services.Interfaces
{
    public interface IRequestService
    {
        void CreateRequest(Request request);
        void ApproveRequest(int id);
    }
}
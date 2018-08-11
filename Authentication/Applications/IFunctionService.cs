using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Applications
{
    using Authentication.Applications.ViewModel;

    public interface IFunctionService
    {
        void Add(FunctionViewModel functionViewModel);

        Task<List<FunctionViewModel>> GetAll(string filter);

        Task<List<FunctionViewModel>> GetAllWithPermission(string userName);

        IEnumerable<FunctionViewModel> GetAllWithParentId(Guid? parentId);

        FunctionViewModel GetById(Guid id);

        void Update(FunctionViewModel functionViewModel);

        void Delete(Guid id);

        void Save();

        bool CheckExistedId(Guid id);

        void UpdateParentId(Guid sourceId, Guid targetId, Dictionary<Guid, int> items);

        void ReOrder(Guid sourceId, Guid targetId);
    }
}

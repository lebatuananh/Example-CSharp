using System;

namespace Authentication.Applications
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Authentication.Applications.ViewModel;
    using Authentication.Entity;
    using Authentication.Entity.EF;

    using global::AutoMapper;
    using global::AutoMapper.QueryableExtensions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class FunctionService : IFunctionService
    {
        private IRepository<Function, Guid> _functionRepository;
        private IRepository<Permission, Guid> _permissionRepository;
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FunctionService(IRepository<Function, Guid> functionRepository, IRepository<Permission, Guid> permissionRepository, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._functionRepository = functionRepository;
            this._permissionRepository = permissionRepository;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public void Add(FunctionViewModel functionViewModel)
        {
            var function = this._mapper.Map<FunctionViewModel, Function>(functionViewModel);
            this._functionRepository.Insert(function);

        }

        public async Task<List<FunctionViewModel>> GetAll(string filter)
        {
            var query = this._functionRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));
            return await query.OrderBy(x => x.ParentId).ProjectTo<FunctionViewModel>().ToListAsync();
        }

        public async Task<List<FunctionViewModel>> GetAllWithPermission(string userName)
        {
            var user = await this._userManager.FindByNameAsync(userName);
            var roles = await this._userManager.GetRolesAsync(user);
            var query = from f in _functionRepository.GetAll()
                        join p in _permissionRepository.GetAll() on f.Id equals p.FunctionId
                        join r in _roleManager.Roles on p.RoleId equals r.Id
                        where roles.Contains(r.Name)
                        select f;
            var parentIds = query.Select(x => x.ParentId).Distinct();
            query = query.Union(_functionRepository.GetAll().Where(f => parentIds.Contains(f.Id)));
            return await query.OrderBy(x => x.ParentId).ProjectTo<FunctionViewModel>().ToListAsync();
        }

        public IEnumerable<FunctionViewModel> GetAllWithParentId(Guid? parentId)
        {
            return this._functionRepository.GetAll(x => x.ParentId == parentId).ProjectTo<FunctionViewModel>();
        }

        public FunctionViewModel GetById(Guid id)
        {
            var function = this._functionRepository.Single(x => x.Id == id);
            return Mapper.Map<Function, FunctionViewModel>(function);
        }

        public void Update(FunctionViewModel functionViewModel)
        {
            var functionDb = _functionRepository.GetById(functionViewModel.Id);
            var function = _mapper.Map<Function>(functionViewModel);
        }

        public void Delete(Guid id)
        {
            this._functionRepository.Delete(id);
        }

        public void Save()
        {
            this._unitOfWork.Commit();
        }

        public bool CheckExistedId(Guid id)
        {
            return this._functionRepository.GetById(id) != null;
        }

        public void UpdateParentId(Guid sourceId, Guid targetId, Dictionary<Guid, int> items)
        {
            //Update parent id for source
            var category = _functionRepository.GetById(sourceId);
            category.ParentId = targetId;
            _functionRepository.Update(category);

            //Get all sibling
            var sibling = _functionRepository.GetAll().Where(x => items.ContainsKey(x.Id));
            foreach (var child in sibling)
            {
                child.SortOrder = items[child.Id];
                _functionRepository.Update(child);
            }
        }

        public void ReOrder(Guid sourceId, Guid targetId)
        {
            var source = _functionRepository.GetById(sourceId);
            var target = _functionRepository.GetById(targetId);
            int tempOrder = source.SortOrder;

            source.SortOrder = target.SortOrder;
            target.SortOrder = tempOrder;

            _functionRepository.Update(source);
            _functionRepository.Update(target);
        }
    }
}
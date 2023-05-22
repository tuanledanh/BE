using MSIA.WebFresher032023.Demo.DL_Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher032023.Demo.DL_Repositories.Repositories
{
    public interface IBaseRepository<TEntity>
    {

        /// <summary>
        /// Hàm gửi kết nối đến database
        /// </summary>
        /// <returns>DbConnection</returns>
        /// Created by: ldtuan (22/05/2023)
        Task<DbConnection> GetOpenConnectionAsync();

        /// <summary>
        /// Hàm lấy 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>TEntity</returns>
        /// Created by: ldtuan (22/05/2023)
        Task<TEntity?> GetAsync(Guid id);

        /// <summary>
        /// Hàm lấy danh sách bản ghi, có phân trang và lọc
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số bản ghi tối đa</param>
        /// <param name="filterName">Tên của bản ghi để thực hiện lọc</param>
        /// <returns>Danh sách TEntity</returns>
        /// Created by: ldtuan (22/05/2023)
        Task<List<TEntity>> GetListAsync(int pageNumber, int pageLimit, string filterName);

        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu của bản ghi muốn thêm mới</param>
        /// <returns>True hoặc false tương ứng với thêm mới thành công hay thất bại</returns>
        /// Created by: ldtuan (22/05/2023)
        Task<bool> PostAsync(TEntity entity);

        /// <summary>
        /// Hàm cập nhật 1 bản ghi có sẵn
        /// </summary>
        /// <param name="id">Id của bản ghi cần cập nhật</param>
        /// <param name="entity">Thông tin mới muốn cập nhật vào bản ghi cũ</param>
        /// <returns>True hoặc false tương ứng với cập nhật thành công hay thất bại</returns>
        /// Created by: ldtuan (22/05/2023)
        Task<bool> PutAsync(Guid id, TEntity entity);

        /// <summary>
        /// Hàm xóa 1 bản ghi có sẵn
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// <returns>True hoặc false tương ứng với xóa thành công hay thất bại</returns>
        /// Created by: ldtuan (22/05/2023)
        Task<bool> DeleteAsync(Guid id);
    }
}

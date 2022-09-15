using Movies.Services.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Services.Reviews
{
    public interface IMovieReviewService
    {
        Task<IList<MovieReviewListModel>> GetAllAsync(int movieId);
        Task<MovieReviewModel> GetAsync(int movieId, int ReviewId);
        Task<MovieReviewModel> Create(int movieId, MovieReviewCreateModel movieReviewCreateModel);
        Task<MovieReviewModel> Update(int ReviewId, MovieReviewCreateModel MovieReviewModel);
        Task<MovieReviewModel> Delete(int ReviewId);
    }
}

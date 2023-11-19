using AutoMapper;
using FS.BLL.Entities;
using FS.BLL.Interfaces;
using FS.WebAPI.Models.Review;
using Microsoft.AspNetCore.Mvc;

namespace FS.WebAPI.Controllers
{
    [Route("api/Reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private IReviewService reviewService;
        private IMapper _mapper;
        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            this.reviewService = reviewService;
            _mapper = mapper;
        }

        // GET: api/<ReviewsController>/5
        [HttpGet("{id}")]
        public async Task<ReviewFullViewModel> GetReview(int id)
        {
            var review = _mapper.Map<Review, ReviewFullViewModel>(await reviewService.GetReview(id));
            return review;
        }

        // GET api/<ReviewsController>
        [HttpGet]
        public async Task<List<ReviewViewModel>> GetReviews()
        {
            var reviews = await reviewService.GetReviews();
            return _mapper.Map<List<Review>, List<ReviewViewModel>>(reviews);
        }

        // POST api/<ReviewsController>
        [HttpPost]
        public async Task<bool> Post(ReviewViewModel review)
        {
            bool result = await reviewService.AddReview(_mapper.Map<ReviewViewModel, Review>(review));
            return result;
        }

        // PUT api/<ReviewsController>/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, ReviewViewModel review)
        {
            bool result = await reviewService.PutReview(id, _mapper.Map<ReviewViewModel, Review>(review));
            return result;
        }

        // DELETE api/<ReviewsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            bool result = await reviewService.DeleteReview(id);
            return result;
        }
    }
}

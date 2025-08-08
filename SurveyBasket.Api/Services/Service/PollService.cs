using SurveyBasket.Api.Error;

namespace SurveyBasket.Api.Services.Service
{
    public class PollService(ApplicationDbcontext context) : IPollservice
    {
        private readonly ApplicationDbcontext _context = context;
        public async Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellationToken)
            => await _context.polls.ProjectToType<PollResponse>().AsNoTracking().ToListAsync(cancellationToken);

        public async Task<IEnumerable<PollResponse>> GetCurrentAsync(CancellationToken cancellationToken = default)
               => await _context.polls
            .Where(x => x.IsPubliched && x.Startsat <= DateOnly.FromDateTime(DateTime.UtcNow) && x.Endsat >= DateOnly.FromDateTime(DateTime.UtcNow))
           .ProjectToType<PollResponse>()
          .AsNoTracking()
            .ToListAsync(cancellationToken);


        public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var poll = await _context.polls.FindAsync(id, cancellationToken);

            if (poll is null)
                return Result<PollResponse>.Failure<PollResponse>(PollErrors.PollNotFound);

            return Result<PollResponse>.Success(poll.Adapt<PollResponse>());

        }


        public async Task<Result<PollResponse>> AddAsync(PollRequest request, CancellationToken cancellationToken = default)
        {
            var Existingpoll = await _context.polls.AnyAsync(p => p.Title == request.Title, cancellationToken);
            if (Existingpoll)
                return Result.Failure<PollResponse>(PollErrors.DuplicatedPollTitle);

            var poll = request.Adapt<Poll>();
            await _context.AddAsync(poll, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<PollResponse>.Success<PollResponse>(poll.Adapt<PollResponse>());
        }

        public async Task<Result> updateAsync(int id, PollRequest request, CancellationToken cancellationToken)
        {
            var Existingpoll = await _context.polls.AnyAsync(p => p.Title == request.Title && p.Id != id, cancellationToken);
            if (Existingpoll)
                return Result.Failure<PollResponse>(PollErrors.DuplicatedPollTitle);

            var _poll = await _context.polls.FindAsync(id, cancellationToken);

            if (_poll is null)
                return Result.Failure(PollErrors.PollNotFound);
            _poll = request.Adapt(_poll);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> deleteAsync(int id, CancellationToken cancellationToken)
        {
            var _poll = await _context.polls.FindAsync(id, cancellationToken);

            if (_poll is null)
                return Result.Failure(PollErrors.PollNotFound);

            _context.polls.Remove(_poll);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> TogglePublishedStatusAsync(int id, CancellationToken cancellationToken)
        {
            var _poll = await _context.polls.FindAsync(id, cancellationToken);

            if (_poll is null)
                return Result.Failure(PollErrors.PollNotFound);

            _poll.IsPubliched = !_poll.IsPubliched;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }


    }
}

using SurveyBasket.Application.Abstractions;

namespace SurveyBasket.Application.Services;

public class QuestionService(
    IApplicationDbContext applicationDbContext,
    ILogger<QuestionService> logger,
    IMapper mapper,
    IValidator<QuestionRequest> validator) : IQuestionService
{
    private readonly IApplicationDbContext _context = applicationDbContext;
    private readonly ILogger<QuestionService> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<QuestionRequest> _validator = validator;

    public async Task<Result<QuestionResponse>> GetQuestionAsync(string pollId, string questionId, CancellationToken cancellationToken)
    {
        var question = await _context.Set<Question>()
            .AsNoTracking()
            .Include(x => x.Answers)
            .Where(x => x.PollId == pollId && x.Id == questionId)
            .ProjectToType<QuestionResponse>()
            .FirstOrDefaultAsync(cancellationToken);

        if (question is null)
        {
            _logger.LogWarning("Question {QuestionId} was not found in poll {PollId}.", questionId, pollId);
            return Result.Failure<QuestionResponse>(QuestionErrors.QuestionNotFound);
        }

        return Result.Success(question);
    }

    public async Task<Result<IEnumerable<QuestionResponse>>> ListQuestionsAsync(string pollId, CancellationToken cancellationToken)
    {
        var questions = await _context.Set<Question>().AsNoTracking()
            .Include(x => x.Answers)
            .Where(x => x.PollId == pollId)
            .ProjectToType<QuestionResponse>()
            .ToListAsync(cancellationToken);

        return Result.Success(_mapper.Map<IEnumerable<QuestionResponse>>(questions));
    }

    public async Task<Result<QuestionResponse>> CreateQuestionAsync(
        string pollId,
        QuestionRequest request,
        CancellationToken cancellationToken)
    {
        if (!await _context.Set<Poll>().AnyAsync(x => x.Id == pollId, cancellationToken))
            return Result.Failure<QuestionResponse>(PollErrors.PollNotFound);

        var question = _mapper.Map<Question>(request);
        question.PollId =pollId;
        var result = await _context.Set<Question>().AddAsync(question, cancellationToken);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating question in poll {PollId}.", pollId);
            return Result.Failure<QuestionResponse>(QuestionErrors.DublicatedQuestion);
        }

        return Result.Success(_mapper.Map<QuestionResponse>(result.Entity));
    }

    public async Task<Result> UpdateQuestionAsync(
        string pollId,
        string questionId,
        QuestionRequest request,
        CancellationToken cancellationToken)
    {
        var isNotValid = await _context.Set<Question>()
            .FirstOrDefaultAsync(x => x.Id != questionId 
                                  && x.PollId == pollId &&
                                  x.Content == request.Content, cancellationToken);

        if (isNotValid is not null)
        {
            _logger.LogWarning("Question with content '{Content}' already exists in poll {PollId}.", request.Content, pollId);
            return Result.Failure(QuestionErrors.DublicatedQuestion);
        }

        var existingQuestion = await _context.Set<Question>()
            .Include(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == questionId && x.PollId == pollId, cancellationToken);
        if (existingQuestion is null)
        {
            _logger.LogWarning("Question {QuestionId} was not found in poll {PollId}.", questionId, pollId);
            return Result.Failure(QuestionErrors.QuestionNotFound);
        }

        // Get the existing answers for the question
        var existingAnswers = existingQuestion.Answers.Select(a=>a.Content).ToList();

        // Get the new answers from the request that are not already in the existing answers
        var newAnswers = request.Answers.Select(a=>a.Content).Except(existingAnswers).ToList();

        // Add the new answers to the existing question
        newAnswers.ForEach(answer =>
        {
            existingQuestion.Answers.Add(new Answer
            {
                Content = answer,
            });
        });

        // Update the IsActive status of existing answers based on the request
        existingQuestion.Answers.ToList().ForEach(answer =>
        {
            answer.IsActive = request.Answers.Any(a => a.Content == answer.Content);
        });        

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteQuestionAsync(string pollId, string questionId, CancellationToken cancellationToken)
    {
        var question = await _context.Set<Question>()
            .FirstOrDefaultAsync(x => x.Id == questionId 
            && x.PollId == pollId, cancellationToken);

        if (question is null)
        {
            _logger.LogWarning("Question {QuestionId} was not found in poll {PollId}.", questionId, pollId);
            return Result.Failure(QuestionErrors.QuestionNotFound);
        }

        _context.Set<Question>().Remove(question);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            _logger.LogError("Failed to delete question {QuestionId} from poll {PollId}.", questionId, pollId);
            return Result.Failure(QuestionErrors.DeleteQuestionConistrants);
        }
        return Result.Success();
    }

    public async Task<Result> ToggleActiveStatusAsync(string pollId, string questionId, CancellationToken cancellationToken)
    {
        var question = await _context.Set<Question>()
            .FirstOrDefaultAsync(x => x.Id == questionId
            && x.PollId == pollId, cancellationToken);

        if (question is null)
        {
            _logger.LogWarning("Question {QuestionId} was not found in poll {PollId}.", questionId, pollId);
            return Result.Failure(QuestionErrors.QuestionNotFound);
        }

        question.IsActive = !question.IsActive;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

}

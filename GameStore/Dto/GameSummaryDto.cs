using System.ComponentModel.DataAnnotations;

namespace GameStore.Dto;

public record GameSummaryDto(
    int Id,
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(30)] string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
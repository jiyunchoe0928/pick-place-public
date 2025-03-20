using System.Collections.Generic;

namespace GraphQLServer.GraphQL.Places.External
{
    public class ExternalDisplayNameDto
    {
        public string? LanguageCode { get; set; }
        public string? Text { get; set; }
    }

    public class ExternalPlaceSummaryDto
    {
        public string? Neutral { get; set; }
        public string? Negative { get; set; }
        public string? Positive { get; set; }
    }

    public class ExternalSummaryDto
    {
        public string? Name { get; set; }
        public ExternalPlaceSummaryDto? SummaryDetail { get; set; }
    }

    public class ExternalAreaDto
    {
        public string? Name { get; set; }
        public string? PlaceId { get; set; }
        public string? Containment { get; set; }
        public ExternalDisplayNameDto? DisplayName { get; set; }
    }

    public class ExternalLandmarkDto
    {
        public string? Name { get; set; }
        public string? PlaceId { get; set; }
        public List<string>? Types { get; set; }
        public double? StraightLineDistanceMeters { get; set; }
        public string? SpatialRelationship { get; set; }
        public ExternalDisplayNameDto? DisplayName { get; set; }
    }

    public class ExternalAddressDescriptorDto
    {
        public List<ExternalAreaDto>? Areas { get; set; }
        public List<ExternalLandmarkDto>? Landmarks { get; set; }
    }

    public class ExternalGoogleMapsLinksDto
    {
        public string? PlaceUri { get; set; }
        public string? ReviewsUri { get; set; }
        public string? PhotosUri { get; set; }
        public string? WriteAReviewUri { get; set; }
        public string? DirectionsUri { get; set; }
    }

    public class ExternalAuthorAttributionDto
    {
        public string? Uri { get; set; }
        public string? DisplayName { get; set; }
        public string? PhotoUri { get; set; }
    }

    public class ExternalPhotoDto
    {
        public string? Name { get; set; }
        public string? FlagContentUri { get; set; }
        public List<ExternalAuthorAttributionDto>? AuthorAttributions { get; set; }
        public string? GoogleMapsUri { get; set; }
        public int? WidthPx { get; set; }
        public int? HeightPx { get; set; }
    }

    public class ExternalReviewDto
    {
        public ExternalDisplayNameDto? OriginalText { get; set; }
        public string? FlagContentUri { get; set; }
        public string? PublishTime { get; set; }
        public string? GoogleMapsUri { get; set; }
        public string? Name { get; set; }
        public double? Rating { get; set; }
        public string? RelativePublishTimeDescription { get; set; }
        public ExternalDisplayNameDto? Text { get; set; }
        public ExternalAuthorAttributionDto? AuthorAttribution { get; set; }
    }

    public class ExternalContextualContentsDto
    {
        public List<ExternalReviewDto>? Reviews { get; set; }
        public List<ExternalPhotoDto>? Photos { get; set; }
    }

    public class ExternalAddressComponentDto
    {
        public List<string>? Types { get; set; }
        public string? LongText { get; set; }
        public string? LanguageCode { get; set; }
        public string? ShortText { get; set; }
    }

    public class ExternalPlusCodeDto
    {
        public string? GlobalCode { get; set; }
        public string? CompoundCode { get; set; }
    }

    public class ExternalAccessibilityOptionsDto
    {
        public bool? WheelchairAccessibleEntrance { get; set; }
        public bool? WheelchairAccessibleParking { get; set; }
    }

    public class ExternalMapPositionDto
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }

    public class ExternalViewportDto
    {
        public ExternalMapPositionDto? Low { get; set; }
        public ExternalMapPositionDto? High { get; set; }
    }

    public class ExternalDatesDto
    {
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? Day { get; set; }
    }

    public class ExternalTimeDetailDto
    {
        public ExternalDatesDto? Date { get; set; }
        public bool? Truncated { get; set; }
        public int? Hour { get; set; }
        public int? Day { get; set; }
        public int? Minute { get; set; }
    }

    public class ExternalPeriodDto
    {
        public ExternalTimeDetailDto? Close { get; set; }
        public ExternalTimeDetailDto? Open { get; set; }
    }

    public class ExternalRegularOpeningHoursDto
    {
        public bool? OpenNow { get; set; }
        public List<ExternalPeriodDto>? Periods { get; set; }
        public List<string>? WeekdayDescriptions { get; set; }
    }

    public class ExternalCurrentOpeningHoursDto
    {
        public bool? OpenNow { get; set; }
        public List<ExternalPeriodDto>? Periods { get; set; }
        public List<string>? WeekdayDescriptions { get; set; }
    }

    public class ExternalPlaceDetailDto
    {
        public ExternalAddressDescriptorDto? AddressDescriptor { get; set; }
        public ExternalDisplayNameDto? DisplayName { get; set; }
        public ExternalGoogleMapsLinksDto? GoogleMapsLinks { get; set; }
        public double? Rating { get; set; }
        public ExternalDisplayNameDto? PrimaryTypeDisplayName { get; set; }
        public List<ExternalPhotoDto>? Photos { get; set; }
        public string? IconMaskBaseUri { get; set; }
        public string? GoogleMapsUri { get; set; }
        public string? FormattedAddress { get; set; }
        public List<ExternalReviewDto>? Reviews { get; set; }
        public string? PrimaryType { get; set; }
        public List<ExternalAddressComponentDto>? AddressComponents { get; set; }
        public int? UserRatingCount { get; set; }
        public string? Id { get; set; }
        public string? NationalPhoneNumber { get; set; }
        public ExternalContextualContentsDto? ContextualContents { get; set; }
        public ExternalDisplayNameDto? EditorialSummary { get; set; }
        public int? UtcOffsetMinutes { get; set; }
        public string? ShortFormattedAddress { get; set; }
        public List<string>? Types { get; set; }
        public ExternalPlusCodeDto? PlusCode { get; set; }
        public string? BusinessStatus { get; set; }
        public bool? GoodForChildren { get; set; }
        public string? AdrFormatAddress { get; set; }
        public ExternalAccessibilityOptionsDto? AccessibilityOptions { get; set; }
        public ExternalViewportDto? Viewport { get; set; }
        public string? IconBackgroundColor { get; set; }
        public string? Name { get; set; }
        public ExternalMapPositionDto? Location { get; set; }
        public string? InternationalPhoneNumber { get; set; }
        public ExternalRegularOpeningHoursDto? RegularOpeningHours { get; set; }
        public ExternalCurrentOpeningHoursDto? CurrentOpeningHours { get; set; }
    }

    public class ExternalPlaceDto
    {
        public string? Key { get; set; }
        public string? Timestamp { get; set; }
        public string? OverallSummary { get; set; }
        public string? SearchUri { get; set; }
        public List<ExternalSummaryDto>? Summaries { get; set; }
        public List<ExternalPlaceDetailDto>? Places { get; set; }
        public string? UpdatedTime { get; set; }
        public string? CurrentState { get; set; }
    }
}
using System.Collections.Generic;

namespace GraphQLServer.GraphQL.Places
{
    // DisplayName
    public class DisplayName
    {
        public required string LanguageCode { get; set; }
        public string? Text { get; set; }
    }

    // PlaceSummary
    public class PlaceSummary
    {
        public string? Neutral { get; set; }
        public string? Negative { get; set; }
        public string? Positive { get; set; }
    }

    // Summary
    public class Summary
    {
        public string? Name { get; set; }
        public PlaceSummary? SummaryDetail { get; set; }
    }

    // Area
    public class Area
    {
        public required string Name { get; set; }
        public required string PlaceId { get; set; }
        public required string Containment { get; set; }
        public required DisplayName DisplayName { get; set; }
    }

    // Landmark
    public class Landmark
    {
        public required string Name { get; set; }
        public required string PlaceId { get; set; }
        public required List<string> Types { get; set; }
        public required double StraightLineDistanceMeters { get; set; }
        public string? SpatialRelationship { get; set; }
        public required DisplayName DisplayName { get; set; }
    }

    // AddressDescriptor
    public class AddressDescriptor
    {
        public required List<Area> Areas { get; set; }
        public required List<Landmark> Landmarks { get; set; }
    }

    // GoogleMapsLinks
    public class GoogleMapsLinks
    {
        public required string PlaceUri { get; set; }
        public required string ReviewsUri { get; set; }
        public required string PhotosUri { get; set; }
        public required string WriteAReviewUri { get; set; }
        public required string DirectionsUri { get; set; }
    }

    // AuthorAttribution
    public class AuthorAttribution
    {
        public required string Uri { get; set; }
        public required string DisplayName { get; set; }
        public required string PhotoUri { get; set; }
    }

    // Photo
    public class Photo
    {
        public required string Name { get; set; }
        public required string FlagContentUri { get; set; }
        public required List<AuthorAttribution> AuthorAttributions { get; set; }
        public required string GoogleMapsUri { get; set; }
        public required int WidthPx { get; set; }
        public required int HeightPx { get; set; }
    }

    // Review
    public class Review
    {
        public required DisplayName OriginalText { get; set; }
        public required string FlagContentUri { get; set; }
        public required string PublishTime { get; set; }
        public required string GoogleMapsUri { get; set; }
        public required string Name { get; set; }
        public required double Rating { get; set; }
        public required string RelativePublishTimeDescription { get; set; }
        public DisplayName? Text { get; set; }
        public required AuthorAttribution AuthorAttribution { get; set; }
    }

    // ContextualContents
    public class ContextualContents
    {
        public List<Review>? Reviews { get; set; }
        public List<Photo>? Photos { get; set; }
    }

    // AddressComponent
    public class AddressComponent
    {
        public required List<string> Types { get; set; }
        public required string LongText { get; set; }
        public required string LanguageCode { get; set; }
        public required string ShortText { get; set; }
    }

    // PlusCode
    public class PlusCode
    {
        public required string GlobalCode { get; set; }
        public required string CompoundCode { get; set; }
    }

    // AccessibilityOptions
    public class AccessibilityOptions
    {
        public bool? WheelchairAccessibleEntrance { get; set; }
        public bool? WheelchairAccessibleParking { get; set; }
    }

    // MapPosition
    public class MapPosition
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }

    // Viewport
    public class Viewport
    {
        public required MapPosition Low { get; set; }
        public required MapPosition High { get; set; }
    }

    // Dates
    public class Dates
    {
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? Day { get; set; }
    }

    // TimeDetail
    public class TimeDetail
    {
        public required Dates Date { get; set; }
        public required bool Truncated { get; set; }
        public required int Hour { get; set; }
        public required int Day { get; set; }
        public required int Minute { get; set; }
    }

    // Period
    public class Period
    {
        public required TimeDetail Close { get; set; }
        public required TimeDetail Open { get; set; }
    }

    // RegularOpeningHours
    public class RegularOpeningHours
    {
        public bool? OpenNow { get; set; }
        public List<Period>? Periods { get; set; }
        public required List<string> WeekdayDescriptions { get; set; }
    }

    // CurrentOpeningHours
    public class CurrentOpeningHours
    {
        public required bool OpenNow { get; set; }
        public required List<Period> Periods { get; set; }
        public required List<string> WeekdayDescriptions { get; set; }
    }

    // PlaceDetail
    public class PlaceDetail
    {
        public required AddressDescriptor AddressDescriptor { get; set; }
        public DisplayName? DisplayName { get; set; }
        public required GoogleMapsLinks GoogleMapsLinks { get; set; }
        public required double Rating { get; set; }
        public DisplayName? PrimaryTypeDisplayName { get; set; }
        public required List<Photo> Photos { get; set; }
        public required string IconMaskBaseUri { get; set; }
        public required string GoogleMapsUri { get; set; }
        public required string FormattedAddress { get; set; }
        public List<Review>? Reviews { get; set; }
        public required string PrimaryType { get; set; }
        public required List<AddressComponent> AddressComponents { get; set; }
        public required int UserRatingCount { get; set; }
        public required string Id { get; set; }
        public required string NationalPhoneNumber { get; set; }
        public required ContextualContents ContextualContents { get; set; }
        public DisplayName? EditorialSummary { get; set; }
        public int? UtcOffsetMinutes { get; set; }
        public required string ShortFormattedAddress { get; set; }
        public required List<string> Types { get; set; }
        public required PlusCode PlusCode { get; set; }
        public required string BusinessStatus { get; set; }
        public bool? GoodForChildren { get; set; }
        public required string AdrFormatAddress { get; set; }
        public required AccessibilityOptions AccessibilityOptions { get; set; }
        public required Viewport Viewport { get; set; }
        public required string IconBackgroundColor { get; set; }
        public required string Name { get; set; }
        public required MapPosition Location { get; set; }
        public string? InternationalPhoneNumber { get; set; }
        public RegularOpeningHours? RegularOpeningHours { get; set; }
        public CurrentOpeningHours? CurrentOpeningHours { get; set; }
    }

    // Place
    public class Place
    {
        public required string Key { get; set; }
        public required string Timestamp { get; set; }
        public required string OverallSummary { get; set; }
        public required string SearchUri { get; set; }
        public List<Summary>? Summaries { get; set; }
        public required List<PlaceDetail> Places { get; set; }
        public required string UpdatedTime { get; set; }
        public required string CurrentState { get; set; }
    }
}
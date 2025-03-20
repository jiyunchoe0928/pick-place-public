export interface PlaceKey {
    key: string;
}

export interface PlaceSummary {
    neutral?: string;
    negative?: string;
    positive?: string;
}

export interface Summary {
    name: string;
    summaryDetail: PlaceSummary;
}

export interface AddressDescriptor {
    areas: Area[];
    landmarks: Landmark[];
}

export interface Area {
    name: string;
    placeId: string;
    containment: string;
    displayName: DisplayName;
}

export interface Landmark {
    name: string;
    placeId: string;
    types: string[];
    straightLineDistanceMeters: number;
    spatialRelationship?: string;
    displayName: DisplayName;
}

export interface DisplayName {
    languageCode: string;
    text: string;
}

export interface GoogleMapsLinks {
    placeUri: string;
    reviewsUri: string;
    photosUri: string;
    writeAReviewUri: string;
    directionsUri: string;
}

export interface Photo {
    name: string;
    flagContentUri: string;
    authorAttributions: AuthorAttribution[];
    googleMapsUri: string;
    widthPx: number;
    heightPx: number;
}

export interface AuthorAttribution {
    uri: string;
    displayName: string;
    photoUri: string;
}

export interface Review {
    originalText: DisplayName;
    flagContentUri: string;
    publishTime: string;
    googleMapsUri: string;
    name: string;
    rating: number;
    relativePublishTimeDescription: string;
    text: DisplayName;
    authorAttribution: AuthorAttribution;
}

export interface ContextualContents {
    reviews: Review[];
    photos?: Photo[];
}

export interface AddressComponent {
    types: string[];
    longText: string;
    languageCode: string;
    shortText: string;
}

export interface PlusCode {
    globalCode: string;
    compoundCode: string;
}

export interface AccessibilityOptions {
    wheelchairAccessibleEntrance?: boolean;
    wheelchairAccessibleParking?: boolean;
}

export interface MapPosition {
    latitude: number;
    longitude: number;
}

export interface Viewport {
    low: MapPosition;
    high: MapPosition;
}
export interface PlaceDetail {
    addressDescriptor: AddressDescriptor;
    displayName: DisplayName;
    googleMapsLinks: GoogleMapsLinks;
    rating: number;
    primaryTypeDisplayName: DisplayName;
    photos: Photo[];
    iconMaskBaseUri: string;
    googleMapsUri: string;
    formattedAddress: string;
    reviews: Review[];
    primaryType: string;
    addressComponents: AddressComponent[];
    userRatingCount: number;
    id: string;
    nationalPhoneNumber: string;
    contextualContents: ContextualContents;
    editorialSummary?: DisplayName;
    utcOffsetMinutes: number;
    shortFormattedAddress: string;
    types: string[];
    plusCode: PlusCode;
    businessStatus: string;
    goodForChildren?: boolean;
    adrFormatAddress: string;
    accessibilityOptions: AccessibilityOptions;
    viewport: Viewport;
    iconBackgroundColor: string;
    name: string;
    location: MapPosition;
    internationalPhoneNumber: string;
    regularOpeningHours?: RegularOpeningHours;
    currentOpeningHours?: CurrentOpeningHours;
}

export interface RegularOpeningHours {
    openNow: boolean;
    periods: Period[];
    weekdayDescriptions: string[];
}

export interface CurrentOpeningHours {
    openNow: boolean;
    periods: Period[];
    weekdayDescriptions: string[];
}

export interface Period {
    close: TimeDetail;
    open: TimeDetail;
}

export interface Dates {
    month: number;
    year: number;
    day: number;
}

export interface TimeDetail {
    date: Dates;
    truncated: boolean;
    hour: number;
    day: number;
    minute: number;
}

export interface PlaceType extends PlaceKey {
    name: string;
    overallSummary: string;
    timestamp: string;
    searchUri: string;
    summaries: Summary[];
    places: PlaceDetail[];
    updatedTime: string;
    currentState: string;
}  
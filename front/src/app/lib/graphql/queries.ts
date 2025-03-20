import { gql } from '@apollo/client';  
  
export const GET_INIT_PLACES = gql`  
  query GET_INIT_PLACES {
    initPlaces {
      key
      overallSummary
      updatedTime
      summaries {
        summaryDetail {
          negative
          neutral
          positive
        }
        name
      }
      places {
        id
        displayName {
          text
        }
        formattedAddress
        googleMapsUri
        googleMapsLinks {
          reviewsUri
        }
        name
        rating
        internationalPhoneNumber
        businessStatus
        primaryTypeDisplayName {
          text
        }
        regularOpeningHours {
          weekdayDescriptions
        }
        userRatingCount
        reviews {
          publishTime
          relativePublishTimeDescription
          rating
          text {
            text
          }
        }
        viewport {
          high {
            latitude
            longitude
          }
          low {
            latitude
            longitude
          }
        }
      }
    }
  }
`;

export const GET_PLACE_BY_MESSAGE = gql`
  query GET_PLACE_BY_MESSAGE($message: String!) {
    placeByMessage(message: $message) {
      key
      overallSummary
      updatedTime
      summaries {
        summaryDetail {
          negative
          neutral
          positive
        }
        name
      }
      places {
        id
        displayName {
          text
        }
        name
        formattedAddress
        googleMapsUri
        googleMapsLinks {
          reviewsUri
        }
        rating
        internationalPhoneNumber
        businessStatus
        primaryTypeDisplayName {
          text
        }
        regularOpeningHours {
          weekdayDescriptions
        }
        reviews {
          publishTime
          relativePublishTimeDescription
          rating
          text {
            text
          }
        }
        userRatingCount
        viewport {
          high {
            latitude
            longitude
          }
          low {
            latitude
            longitude
          }
        }
      }
    }
  }
`
# pick-place-dotnet

**.NET 기반 GraphQL을 사용해 볼 목적으로 진행한 프로젝트입니다.  
기존에 작업했던 포트폴리오용 프로젝트의 일부를 마이그레이션하는 방식으로 진행했으며,  
프론트앱과 웹앱 외에 실제 API를 활용한 데이터 처리는 별개로 구성되어 있습니다.**

## 목차

- [프로젝트 소개](#프로젝트-소개)
- [주요 기능](#주요-기능)
- [기술 스택](#기술-스택)
- [사용 방법](#사용-방법)

## 프로젝트 소개

지역과 키워드를 포함해서 검색하면 해당 조건과 유사한 장소를 추천하고,<br>
각각의 장소들에 대한 리뷰 요약과 전체 요약을 제공하는 서비스의 .NET 서버입니다.<br>

오직 .NET GraphQL을 사용해 볼 목적이므로, 기존에 nodejs를 기반으로 설계했던 서버에서<br>
GraphQL 부분만 .NET으로 마이그레이션을 진행하였습니다.<br>

스키마의 경우, Google API를 사용했기에 해당 API를 기반으로 작성되었습니다.

## 주요 기능

- 기능 1: InitPlaces<br>첫 페이지에서 표기할 추천 검색 장소 리스트를 서버에서 받아 받환합니다.<br>캐싱된 데이터가 있을 경우, 해당 데이터를 반환합니다.<details>
  <summary>GraphQL 쿼리 예제</summary>

  ```graphql
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
  ```
</details>

- 기능 2: placeByMessage<br>유저의 메세지를 클라이언트에게서 전달 받고, 서버에서 데이터를 받아 반환합니다.<br>메세지 또는 키워드와 일치하는 데이터가 있을 경우 캐싱된 데이터를 반환합니다.<details>
  <summary>GraphQL 쿼리 예제</summary>

  ```graphql
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
  ```
</details>


## 기술 스택

*   **언어:** C#
*   **프레임워크:** ASP.NET Core
*   **라이브러리:** HotChocolate.AspNetCore (GraphQL 서버 구현)


## 사용 방법

mongodb와 redis가 구성된 환경에서 dotnet run을 실행하면<br>
http://localhost:5112/graphql/ 에서 쿼리를 확인해보실 수 있습니다.

실제 사용은 불가능하며 https://portfolio.pickplace.help/ 에서 확인해 보실 수 있습니다. <br>
(appsettings - ExternalUrlStrings에 API와 LLM을 사용하는 서버 주소가 비워져 있습니다.)
import React, { useState } from 'react';  
import { PlaceSummary, PlaceType } from '../types/PlaceType';  
  
interface SearchResultsProps {  
  searchPlace: PlaceType;  
}  
  
const SearchResults: React.FC<SearchResultsProps> = ({ searchPlace }) => {  
  const [reviewIndices, setReviewIndices] = useState<{ [key: string]: number }>({});  
  const [activeSummaries, setActiveSummaries] = useState<{ [placeId: string]: keyof PlaceSummary | null }>({});  
  
  const handlePreviousReview = (placeId: string, reviewsLength: number) => {  
    setReviewIndices((prevIndices) => ({  
      ...prevIndices,  
      [placeId]: prevIndices[placeId] > 0 ? prevIndices[placeId] - 1 : reviewsLength - 1,  
    }));  
  };  
  
  const handleNextReview = (placeId: string, reviewsLength: number) => {  
    setReviewIndices((prevIndices) => ({  
      ...prevIndices,  
      [placeId]: prevIndices[placeId] < reviewsLength - 1 ? prevIndices[placeId] + 1 : 0,  
    }));  
  };
  
  const handleSummaryHover = (placeId: string, summaryType: keyof PlaceSummary | null) => {  
    setActiveSummaries((prevSummaries) => ({  
      ...prevSummaries,  
      [placeId]: summaryType,
    }));  
  };    

  
  return (  
    <div className="w-full max-w-4xl mt-6 bg-white rounded shadow-md p-4">  
      <h2 className="text-lg font-bold mb-4">まとめ</h2>  
      <div className="mb-6">  
        {searchPlace.overallSummary !== '' ? (  
          <p className="text-gray-700 text-sm bg-gray-100 p-3 rounded">  
            {searchPlace.overallSummary}  
          </p>  
        ) : (  
          <div className="flex items-center space-x-2">  
            <div className="animate-spin rounded-full h-4 w-4 border-t-2 border-b-2 border-gray-500"></div>  
            <span className="text-sm text-gray-500">データを取りまとめています</span>  
          </div>  
        )}  
      </div>  
  
      <h2 className="text-lg font-bold mb-4">おすすめスポット</h2>  
      {searchPlace.places.length > 0 ? (  
        <ul className="grid grid-cols-1 sm:grid-cols-2 gap-4">  
          {searchPlace.places.map((place, index) => {  
            const reviews = (place.reviews || []).filter(  
              (review) => review?.text?.text && review.text.text.trim() !== ''  
            );  
            const currentReviewIndex = reviewIndices[place.id] || 0;  

            const validReviewSummary = searchPlace.summaries && (searchPlace.summaries[index]?.name === searchPlace.places[index].name
                                       && (searchPlace.summaries[index].summaryDetail.positive !== ""
                                        || searchPlace.summaries[index].summaryDetail.neutral !== ""
                                        || searchPlace.summaries[index].summaryDetail.negative !== ""
                                       ));
  
            return (  
              <li  
                key={place.displayName?.text}  
                className="p-4 border border-gray-300 rounded-lg shadow-sm flex flex-col"  
              >  
                {/* 구글 지도 임베딩 섹션 */}  
                <div className="mb-3">
                  {place.googleMapsUri ? (  
                    <iframe  
                      src={place.googleMapsUri + '&output=embed'}  
                      className="w-full h-48 rounded-lg"  
                      allowFullScreen  
                      loading="lazy"  
                      referrerPolicy="no-referrer-when-downgrade"  
                      title={place.displayName?.text}  
                    ></iframe>  
                  ) : (  
                    <div className="w-full h-48 bg-gray-200 rounded-lg flex items-center justify-center">  
                      <span className="text-gray-500">Unable to display map</span>  
                    </div>  
                  )}  
                </div>  
  
                {/* 장소 정보 섹션 */}  
                <div>  
                  <h3 className="text-base font-semibold mb-1">{place.displayName?.text}</h3>  
                  <p className="text-sm text-gray-700 mb-2">{place.formattedAddress}</p>  
                </div>  
                
                {/* 추가 정보 섹션 */}  
                <div  
                  className="mt-3 mt-auto"  
                  style={{ whiteSpace: 'pre-wrap', fontFamily: 'monospace' }}  
                >  
                  {place.businessStatus && (  
                    <p className="text-sm text-green-600">  
                      {place.businessStatus === 'OPERATIONAL' ? '営業中' : '営業終了'}  
                    </p>  
                  )}  
                  {place.regularOpeningHours &&  
                    place.regularOpeningHours.weekdayDescriptions && (  
                      <div className="text-sm text-gray-500">  
                      <span>{'営業時間:'}</span> 
                        {place.regularOpeningHours.weekdayDescriptions.map((item, index) => (  
                          <div key={index}>  
                            {item}  
                          </div>  
                        ))}  
                      </div>  
                    )}  
                </div>  
  
                {/* 평점 섹션 (하단 정렬) */}  
                {place.rating && (  
                  <div className="flex items-center text-sm text-yellow-500">  
                    <span>⭐{place.rating}</span>  
                    <span className="text-gray-500 ml-2"> (レビュー{place.userRatingCount}件)</span>  
                  </div>  
                )}

                {/* 요약 섹션 */}  
                {validReviewSummary ? (  
                  <div className="mt-3">  
                    <p className="text-sm text-gray-500 font-semibold mb-2">レビューのポイント</p>  
                    <div className="flex space-x-2 w-full">  
                    {/* Positive 버튼 */}  
                    <div className="relative flex-1">  
                      <button  
                        onMouseEnter={() => handleSummaryHover(place.id, 'positive')}  
                        onMouseLeave={() => handleSummaryHover(place.id, null)}  
                        disabled={searchPlace.summaries[index].summaryDetail.positive === ''}  
                        className={`w-full p-2 rounded text-sm ${  
                          searchPlace.summaries[index].summaryDetail.positive === ''  
                            ? 'bg-gray-300 text-gray-500 cursor-not-allowed'  
                            : 'bg-green-100 text-green-700'  
                        }`}  
                      >  
                        😊  
                      </button>  
                      {activeSummaries[place.id] === 'positive' && (  
                        <div  
                          className="absolute top-[-50px] left-0 w-48 p-2 bg-green-100 text-green-700 text-sm rounded shadow-md pointer-events-none z-50"  
                        >  
                          {searchPlace.summaries[index].summaryDetail.positive}  
                        </div>  
                      )}  
                    </div>  
                    
                    {/* Neutral 버튼 */}  
                    <div className="relative flex-1">  
                      <button  
                        onMouseEnter={() => handleSummaryHover(place.id, 'neutral')}  
                        onMouseLeave={() => handleSummaryHover(place.id, null)}  
                        disabled={searchPlace.summaries[index].summaryDetail.neutral === ''}  
                        className={`w-full p-2 rounded text-sm ${  
                          searchPlace.summaries[index].summaryDetail.neutral === ''  
                            ? 'bg-gray-300 text-gray-500 cursor-not-allowed'  
                            : 'bg-white text-gray-700 border border-gray-300'  
                        }`}  
                      >  
                        😐  
                      </button>  
                      {activeSummaries[place.id] === 'neutral' && (  
                        <div  
                          className="absolute top-[-50px] left-0 w-48 p-2 bg-gray-100 text-gray-700 text-sm rounded shadow-md pointer-events-none z-50"  
                        >  
                          {searchPlace.summaries[index].summaryDetail.neutral}  
                        </div>  
                      )}  
                    </div>  
                    
                    {/* Negative 버튼 */}  
                    <div className="relative flex-1">  
                      <button  
                        onMouseEnter={() => handleSummaryHover(place.id, 'negative')}  
                        onMouseLeave={() => handleSummaryHover(place.id, null)}  
                        disabled={searchPlace.summaries[index].summaryDetail.negative === ''}  
                        className={`w-full p-2 rounded text-sm ${  
                          searchPlace.summaries[index].summaryDetail.negative === ''  
                            ? 'bg-gray-300 text-gray-500 cursor-not-allowed'  
                            : 'bg-red-100 text-red-700'  
                        }`}  
                      >  
                        😡  
                      </button>  
                      {activeSummaries[place.id] === 'negative' && (  
                        <div  
                          className="absolute top-[-50px] left-0 w-48 p-2 bg-red-100 text-red-700 text-sm rounded shadow-md pointer-events-none z-50"  
                        >  
                          {searchPlace.summaries[index].summaryDetail.negative}  
                        </div>  
                      )}  
                    </div>  
                  </div>    
                  </div>  
                ) : (
                  <p className="text-sm text-gray-500 font-semibold mb-2">レビューを分析中です...</p>  
                )}
  
                {/* 리뷰 섹션 */}  
                {reviews.length > 0 && (
                  <div  
                    className="relative bg-gray-100 p-4 rounded-lg shadow-md mt-4 cursor-pointer group"  
                    onClick={() => {  
                      if (place.googleMapsLinks?.reviewsUri) {  
                        window.open(place.googleMapsLinks.reviewsUri, '_blank');  
                      }  
                    }}  
                  >  
                  {/* 리뷰 텍스트 */}  
                    <p  
                      className="text-sm text-gray-700 line-clamp-2 overflow-hidden"  
                      style={{  
                        display: '-webkit-box',  
                        WebkitBoxOrient: 'vertical',  
                        WebkitLineClamp: 2,
                      }}  
                    >  
                      {reviews[currentReviewIndex]?.text?.text}  
                    </p>  
                    {/* 좌우 버튼 */}  
                    <div  
                      className="absolute top-1/2 transform -translate-y-1/2 left-2 opacity-0 group-hover:opacity-100 transition-opacity duration-300"  
                    >  
                      <button  
                        onClick={(e) => {  
                          e.stopPropagation();
                          handlePreviousReview(place.id, reviews.length);  
                        }}  
                        className="bg-gray-300 rounded-full p-2 shadow-md"  
                      >  
                        &lt;  
                      </button>  
                    </div>  
                    <div  
                      className="absolute top-1/2 transform -translate-y-1/2 right-2 opacity-0 group-hover:opacity-100 transition-opacity duration-300"  
                    >  
                      <button  
                        onClick={(e) => {  
                          e.stopPropagation();
                          handleNextReview(place.id, reviews.length);  
                        }}  
                        className="bg-gray-300 rounded-full p-2 shadow-md"  
                      >  
                        &gt;  
                      </button>  
                    </div>  
                  </div>  
                )}  
              </li>  
            );  
          })}  
        </ul>  
      ) : (  
        <p className="text-sm text-gray-500">残念ながら、見つかりませんでした。</p>  
      )}  
    </div>  
  );  
};  
  
export default SearchResults;  
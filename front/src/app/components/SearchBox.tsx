'use client';
import React, { useState, useEffect } from 'react';  
import ButtonGroup from './ButtonGroup';  
import SearchResults from './SearchResults';  
import { PlaceType } from '../types/PlaceType';  
import { searchPlace } from '../lib/graphql/services/places';
  
interface SearchBoxProps {  
  initialPlaces: PlaceType[];  
}  
  
const SearchBox: React.FC<SearchBoxProps> = ({ initialPlaces }) => {  
  const [query, setQuery] = useState<string>('');
  const [searchResult, setSearchResult] = useState<PlaceType | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  
  useEffect(() => {  
    let intervalId: NodeJS.Timeout;  
    
    const fetchUpdatedData = async () => {  
      if (searchResult && searchResult.overallSummary === '') {  
        try {  
          const updatedPlace = await searchPlace(query);
    
          if (updatedPlace.overallSummary !== '') {  
            setSearchResult(updatedPlace);  
          }  
        } catch (err) {  
          console.error('ERROR:', err);  
          setError('データの取得に失敗しました。');  
        }  
      }  
    };  
    
    const timeoutId = setTimeout(() => {  
      fetchUpdatedData();  
      intervalId = setInterval(fetchUpdatedData, 2000);
    }, 5000);
    
    return () => {  
      clearTimeout(timeoutId);  
      clearInterval(intervalId);  
    };  
  }, [searchResult, query]);   
  
  const handleSearch = async () => {  
    if (!query.trim()) return;  
  
    setLoading(true);  
    setError(null);  
  
    try {  
      const place = await searchPlace(query);
  
      setSearchResult(place);  
    } catch (err) {  
      console.error('ERROR:', err);  
      setError('データの取得に失敗しました。');  
    } finally {  
      setLoading(false);  
    }  
  };  
  
  return (  
    <div className="w-full max-w-4xl mt-6 bg-white rounded-lg shadow-lg p-6 mx-auto">  
      <div className="flex items-center border border-gray-300 rounded-full shadow-sm p-3 bg-white">  
        <input  
          type="text"  
          value={query}  
          onChange={(e) => setQuery(e.target.value)}  
          placeholder="気になるエリアで、お目当ての場所を探してみませんか？"  
          className="flex-grow outline-none px-4 text-sm"  
          onKeyDown={(e) => {  
            if (e.key === 'Enter') {  
              handleSearch();  
            }  
          }}  
        />  
        <button  
          onClick={handleSearch}  
          className="ml-2 text-gray-500 hover:text-gray-700 transition"  
        >  
          <svg  
            xmlns="http://www.w3.org/2000/svg"  
            fill="none"  
            viewBox="0 0 24 24"  
            strokeWidth={1.5}  
            stroke="currentColor"  
            className="w-5 h-5"  
          >  
            <path strokeLinecap="round" strokeLinejoin="round" />  
          </svg>  
        </button>  
      </div>  
  
      {loading ? (  
        <p className="text-gray-500 mt-4">検索中…</p>  
      ) : error ? (  
        <p className="text-red-500 mt-4">{error}</p>  
      ) : searchResult ? (  
        <SearchResults searchPlace={searchResult} />  
      ) : (  
        <ButtonGroup places={initialPlaces} onSelectPlace={setSearchResult} />  
      )}  
    </div>  
  );  
};  
  
export default SearchBox;  
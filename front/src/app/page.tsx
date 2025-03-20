import React from 'react';  
import SearchBox from './components/SearchBox';  
import { getInitPlace } from './lib/graphql/services/places';
  

export default async function Home() {  
  const places = await getInitPlace();

  return (  
    <main className="flex flex-col items-center justify-center min-h-screen bg-gray-50 px-4">  
      <h1 className="text-2xl font-bold mb-6 text-center mt-16">  
        どんな場所を見つけたいですか？
      </h1>  
        <SearchBox initialPlaces={places} />  
    </main>  
  );   
}  
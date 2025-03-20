'use client';  
  
import React from 'react';  
import { PlaceType } from '../types/PlaceType';  
  
interface ButtonGroupProps {  
  places: PlaceType[];  
  onSelectPlace: (place: PlaceType) => void;
}  
  
const ButtonGroup: React.FC<ButtonGroupProps> = ({ places, onSelectPlace }) => {  
  return (  
    <div className="w-full flex flex-col items-center mt-6">  
      <div className="flex flex-wrap justify-center gap-3">
        {places.map((place) => (  
          <button  
            key={place.key}  
            onClick={() => onSelectPlace(place)}
            className="flex items-center gap-2 px-4 py-2 bg-gray-200 rounded-full text-sm font-medium shadow-sm hover:bg-gray-300 transition"  
          >  
            {place.name}
          </button>  
        ))}  
      </div>  
    </div>  
  );  
};  
  
export default ButtonGroup;  
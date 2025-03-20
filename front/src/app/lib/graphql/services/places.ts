import { GET_PLACE_BY_MESSAGE, GET_INIT_PLACES } from '../queries';
import client from '../../apollo-client';  
import { convertKeyToName } from '@/app/utils/utils';
import { PlaceType } from '@/app/types/PlaceType';

export async function getInitPlace() {  
  const { data } = await client.query({  
    query: GET_INIT_PLACES,
    fetchPolicy: 'no-cache',
  });  

  return data.initPlaces.map((place: PlaceType) => { 
      return {
        ...place,
        name: convertKeyToName(place.key)
      }
  });
}  

export async function searchPlace(search: string) {  
  const { data } = await client.query({  
    query: GET_PLACE_BY_MESSAGE,  
    variables: { message: search }, 
    fetchPolicy: 'no-cache',
  });  

  return {
    ...data.placeByMessage,
    name: convertKeyToName(data.placeByMessage.key)
  };  
}  
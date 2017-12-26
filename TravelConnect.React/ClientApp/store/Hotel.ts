import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface HotelState {
  searchRequestHotel: any
  isRateChange: boolean
  recheckedPrice: any
  selectedHotel: any
  selectedRoom: any
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

interface SetSearchAction { type: 'SET_SEARCH', searchRequest: any }
interface SetRateChangeAction { type: 'SET_RATE_CHANGE' }
interface SetRateUnchangeAction { type: 'SET_RATE_UNCHANGE' }
interface SetRecheckedPriceAction { type: 'SET_RECHECKED_PRICE', recheckedPrice: any }
interface SetSelectedHotelAction { type: 'SET_SELECTED_HOTEL', selectedHotel: any }
interface SetSelectedRoomAction { type: 'SET_SELECTED_ROOM', selectedRoom: any }


// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = SetSearchAction | SetRateUnchangeAction | SetRateChangeAction | SetRecheckedPriceAction | SetSelectedHotelAction | SetSelectedRoomAction

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).
export const actionCreators = {
  setSearch: (searchRequest: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_SEARCH', searchRequest: searchRequest })
  },
  setRateChange: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_RATE_CHANGE' })
  },
  setRateUnchange: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_RATE_UNCHANGE' })
  },
  setRecheckedPrice: (recheckedPrice: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_RECHECKED_PRICE', recheckedPrice: recheckedPrice })
  },
  setSelectedHotel: (selectedHotel: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_SELECTED_HOTEL', selectedHotel: selectedHotel })
  },
  setSelectedRoom: (selectedRoom: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_SELECTED_ROOM', selectedRoom: selectedRoom })
  },
}

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<HotelState> = (state: HotelState, action: KnownAction) => {
  switch (action.type) {
    case 'SET_SEARCH':
      return { ...state, searchRequestHotel: action.searchRequest };
    case 'SET_RATE_CHANGE':
      return { ...state, isRateChnage: true };
    case 'SET_RATE_UNCHANGE':
      return { ...state, isRateChnage: false };
    case 'SET_RECHECKED_PRICE':
      return { ...state, recheckedPrice: action.recheckedPrice }
    case 'SET_SELECTED_HOTEL':
      return { ...state, selectedHotel: action.selectedHotel };
    case 'SET_SELECTED_ROOM':
      return { ...state, selectedRoom: action.selectedRoom };
    default:
      // The following line guarantees that every action in the KnownAction union has been covered by a case above
      const exhaustiveCheck: never = action;
  }

  // For unrecognized actions (or in cases where actions have no effect), must return the existing state
  //  (or default initial state if none was supplied)
  return state || { searchRequestHotel: null, selectedHotel: null, selectedRoom: null, isRateChange: false, recheckedPrice: null };
};
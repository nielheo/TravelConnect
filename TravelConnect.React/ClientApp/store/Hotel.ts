import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface HotelState {
  searchRequest: any
  isRateChange: boolean
  selectedRoom: any
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

interface SetSearchAction { type: 'SET_SEARCH', searchRequest: any }
interface SetRateChangeAction { type: 'SET_RATE_CHANGE' }
interface SetSelectedReturnAction { type: 'SET_SELECTED_ROOM', selectedReturn: any }

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = SetSearchAction | SetRateChangeAction | SetSelectedReturnAction

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).
export const actionCreators = {
  setSearch: (searchRequest: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_SEARCH', searchRequest: searchRequest })
  },
  setIsRateChnage: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_RATE_CHANGE' })
  },
  setSelectedReturn: (selectedReturn: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_SELECTED_ROOM', selectedReturn: selectedReturn })
  },
}

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<HotelState> = (state: HotelState, action: KnownAction) => {
  switch (action.type) {
    case 'SET_SEARCH':
      return { ...state, searchRequest: action.searchRequest };
    case 'SET_RATE_CHANGE':
      return { ...state, isRateChnage: true };
    case 'SET_SELECTED_ROOM':
      return { ...state, selectedReturn: action.selectedReturn };
    default:
      // The following line guarantees that every action in the KnownAction union has been covered by a case above
      const exhaustiveCheck: never = action;
  }

  // For unrecognized actions (or in cases where actions have no effect), must return the existing state
  //  (or default initial state if none was supplied)
  return state || { searchRequest: null, selectedRoom: null, isRateChange: false };
};
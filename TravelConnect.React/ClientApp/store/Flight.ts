import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.


export interface FlightState {
  searchRequest: any;
  searchResult: any[];
  isReturnFlight: boolean;
  selectedDeparture: any;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

interface SetSearchAction { type: 'SET_SEARCH', searchRequest: any }
interface SetResultAction { type: 'SET_RESULT', searchResult: any }
interface SetIsReturnFlightAction { type: 'SET_IS_RETURN_FLIGHT' }
interface SetSelectedDepartureAction { type: 'SET_SELECTED_DEPARTURE', selectedDeparture: any }

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = SetSearchAction | SetResultAction | SetIsReturnFlightAction | SetSelectedDepartureAction

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
  setSearch: (searchRequest: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_SEARCH', searchRequest: searchRequest })
  },
  setResult: (searchResult: any[]): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_RESULT', searchResult: searchResult })
  },
  setIsReturnFlight: (isReturnFlight: boolean): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_IS_RETURN_FLIGHT' })
  },
  setSelectedDeparture: (selectedDeparture: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
    dispatch({ type: 'SET_SELECTED_DEPARTURE', selectedDeparture: selectedDeparture })
  },
}

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<FlightState> = (state: FlightState, action: KnownAction) => {
  switch (action.type) {
    case 'SET_SEARCH':
      return { ...state, searchRequest: action.searchRequest };
    case 'SET_RESULT':
      return { ...state, searchResult: action.searchResult };
    case 'SET_IS_RETURN_FLIGHT':
      return { ...state, isReturnFlight: true };
    case 'SET_SELECTED_DEPARTURE':
      console.log(action.selectedDeparture)
      return { ...state, selectedDeparture: action.selectedDeparture };
    default:
      // The following line guarantees that every action in the KnownAction union has been covered by a case above
      const exhaustiveCheck: never = action;
  }

  // For unrecognized actions (or in cases where actions have no effect), must return the existing state
  //  (or default initial state if none was supplied)
  return state || { search: null, result: null, returnFlight: false, selectedDepart: null };
};

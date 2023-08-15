export const getMessageFromError = (error: any): string => {
    return error.response.data.error
}
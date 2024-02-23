export const getMessageFromError = (error: any): string => {
    console.log(error)
    return error.response.data.error
}